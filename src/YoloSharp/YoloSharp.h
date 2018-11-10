// OpenCVYOLO.h

#pragma once
#include <opencv2/dnn.hpp>
#include <opencv2/dnn/shape_utils.hpp>
#include <opencv2/imgproc.hpp>
#include <opencv2/highgui.hpp>
using namespace cv;
using namespace cv::dnn;

#include <fstream>
#include <iostream>
#include <algorithm>
#include <cstdlib>

using namespace std;
using namespace System;
using namespace System::Drawing;
using namespace System::Collections::Generic;
using namespace System::IO;


#include <msclr/gcroot.h>

// Darknet YOLOv2 .NET Framework C# wrapper (for OpenCV 3.3.1/3.4) 
namespace YoloSharp {
	public enum class Target : int { CPU, OpenCL, OpenCLFp16, VPU };
	public enum class Backend : int { Auto, Halide, OpenVINO, OpenCV };
	public ref class Data
	{
	public:
		System::String^ Name;
		int Id;
		float Confidence;
		int X;
		int Y;
		int Width;
		int Height;

	public:
		Data(System::String^ name, System::Int32 id, System::Single confidence, System::Int32 x, System::Int32 y, System::Int32 width, System::Int32 height)
		{
			Name = name;
			Id = id;
			Confidence = confidence;
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}
	};
	/// <summary>
	/// Darknet YOLOv2 C# wrapper
	/// </summary>
	public ref class Yolo
	{
	internal:
		const size_t network_width = 416;
		const size_t network_height = 416;
		const float default_threshold = 0.24f;
		System::String^ _config;
		System::String^ _weights;
		cli::array<System::String^>^ _names;
		dnn::Net *_net;

	public:
		/// <summary>
		/// Constructor
		/// </summary>
		Yolo(System::String^ config, System::String^ weights, System::String^ names){
			initialize(config, weights, names);
		}
		/// <summary>
		/// Destructor
		/// </summary>
		~Yolo() {
			delete _net;
		}
		/// <summary>
		/// Detect Objects
		/// </summary>
		cli::array<Data^>^ Detect(Bitmap^ bitmap) {
			return detectMain(bitmap, default_threshold);
		}
		/// <summary>
		/// Detect Objects
		/// </summary>
		cli::array<Data^>^ Detect(Bitmap^ bitmap, float confidenceThreshold) {
			return detectMain(bitmap, confidenceThreshold);
		}
		/// <summary>
		/// Return class names of this model
		/// </summary>
		property cli::array<System::String^>^ ClassNames {
			cli::array<System::String^>^ get(){
				return _names;
			}
		}
		void SetPreferableBackend(Backend backend) {
			_net->setPreferableTarget((int)backend);
		}
		void SetPreferableTarget(Target target) {
			_net->setPreferableTarget((int)target);
		}
	private:
		void initialize(System::String^ config, System::String^ weights, System::String^ names) {
			_config = config;
			_weights = weights;
			_names = File::ReadAllLines(names);

			std::string modelConfiguration = convertToStdString(_config);
			std::string modelBinary = convertToStdString(_weights);

			this->_net = new dnn::Net(readNetFromDarknet(modelConfiguration, modelBinary));
		}

		cli::array<Data^>^ detectMain(Bitmap^ bitmap, float confidenceThreshold) {
			auto iplImage = getIplImage(bitmap);
			cv::Mat frame = cv::cvarrToMat(iplImage);
			cv::Mat resized;
			cv::resize(frame, resized, cv::Size(network_width, network_height));

			cv::Mat inputBlob = blobFromImage(resized, 1 / 255.F);
			_net->setInput(inputBlob, (cv::String) "data");

			cv::Mat detectionMat = _net->forward((cv::String) "detection_out");

			List<Data^>^ results = gcnew List<Data^>(detectionMat.rows);
			for (int i = 0; i < detectionMat.rows; i++)
			{
				const int probability_index = 5;
				const int probability_size = detectionMat.cols - probability_index;
				float *prob_array_ptr = &detectionMat.at<float>(i, probability_index);

				size_t objectClass = std::max_element(prob_array_ptr, prob_array_ptr + probability_size) - prob_array_ptr;
				float confidence = detectionMat.at<float>(i, (int)objectClass + probability_index);

				if (confidence > confidenceThreshold)
				{
					float cx = detectionMat.at<float>(i, 0);
					float cy = detectionMat.at<float>(i, 1);
					float width = detectionMat.at<float>(i, 2);
					float height = detectionMat.at<float>(i, 3);

					int xx = (int)((cx - width / 2.) * frame.cols);
					int yy = (int)((cy - height / 2.) * frame.rows);
					int ww = (int)(width * frame.cols);
					int hh = (int)(height * frame.rows);
					Data^ d = gcnew Data(_names[objectClass], objectClass, confidence, xx, yy, ww, hh);
					results->Add(d);
				}
			}
			cvReleaseImage(&iplImage);
			return results->ToArray();
		}
		 
		std::string convertToStdString(System::String^ str)
		{
			std::string stdStr;
			marshalString(str, stdStr);
			return stdStr;
		}
		void marshalString(System::String ^ s, string& os) {
			using namespace Runtime::InteropServices;
			const char* chars = (const char*)(Marshal::StringToHGlobalAnsi(s)).ToPointer();
			os = chars;
			Marshal::FreeHGlobal(IntPtr((void*)chars));
		}
		IplImage* getIplImage(Bitmap^ bitmap) {
			IplImage *image = cvCreateImage(cvSize(bitmap->Width, bitmap->Height), IPL_DEPTH_8U, 3);

			Imaging::BitmapData^ data = bitmap->LockBits(
				Rectangle(0, 0, bitmap->Width, bitmap->Height),
				Imaging::ImageLockMode::ReadOnly,
				Imaging::PixelFormat::Format24bppRgb
			);
			memcpy(image->imageData, data->Scan0.ToPointer(), image->imageSize);
			bitmap->UnlockBits(data);

			return image;
		}
	};
}

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

// Darknet YOLOv2/v3 .NET Framework C# wrapper (for OpenCV 3.3.1/3.4) 
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
	/// Darknet YOLOv2/v3 C# wrapper
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
		vector<cv::String> *_layerNames;

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
		/// Detect Objects and apply NMS
		/// </summary>
		cli::array<Data^>^ Detect(Bitmap^ bitmap, float confidenceThreshold, float NMSThreshold) {
			return detectMain(bitmap, confidenceThreshold, NMSThreshold);
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
			vector<int> outLayers = _net->getUnconnectedOutLayers();

			this->_layerNames = new vector<cv::String>();
			vector<cv::String> layersNames = _net->getLayerNames();
			_layerNames->resize(outLayers.size());
			for (size_t i = 0; i < outLayers.size(); ++i)
			{
				cv::String layerName = layersNames[outLayers[i] - 1];
				(*_layerNames)[i] = layerName;
			}
		}

		cli::array<Data^>^ detectMain(Bitmap^ bitmap, float confidenceThreshold) {
			auto iplImage = getIplImage(bitmap);
			cv::Mat frame = cv::cvarrToMat(iplImage);
			cv::Mat resized;
			cv::resize(frame, resized, cv::Size(network_width, network_height));

			cv::Mat inputBlob = blobFromImage(resized, 1 / 255.F);
			_net->setInput(inputBlob, (cv::String) "data");

			vector<Mat> detectionMat;
			_net->forward(detectionMat, *_layerNames);
			List<Data^>^ results = gcnew List<Data^>();
			for (size_t i = 0; i < detectionMat.size(); ++i)
			{
				float* data = (float*)detectionMat[i].data;
				for (int j = 0; j < detectionMat[i].rows; ++j, data += detectionMat[i].cols)
				{
					Mat scores = detectionMat[i].row(j).colRange(5, detectionMat[i].cols);
					cv::Point classIdPoint;
					double confidence;
					minMaxLoc(scores, 0, &confidence, 0, &classIdPoint);
					if (confidence > confidenceThreshold)
					{
						int centerX = (int)(data[0] * frame.cols);
						int centerY = (int)(data[1] * frame.rows);
						int width = (int)(data[2] * frame.cols);
						int height = (int)(data[3] * frame.rows);
						int left = centerX - width / 2;
						int top = centerY - height / 2;

						Data^ d = gcnew Data(_names[classIdPoint.x], classIdPoint.x, (float)confidence, left, top, width, height);
						results->Add(d);
					}
				}
			}
			cvReleaseImage(&iplImage);
			return results->ToArray();
		}

		cli::array<Data^>^ detectMain(Bitmap^ bitmap, float confidenceThreshold, float NMSThreshold) {
			auto iplImage = getIplImage(bitmap);
			cv::Mat frame = cv::cvarrToMat(iplImage);
			cv::Mat resized;
			cv::resize(frame, resized, cv::Size(network_width, network_height));

			// set input
			cv::Mat inputBlob = blobFromImage(resized, 1 / 255.F);
			_net->setInput(inputBlob, (cv::String) "data");

			// detection
			vector<Mat> detectionMat;
			_net->forward(detectionMat, *_layerNames);

			// Store information to perform NMS 
			std::vector<Rect> boxes;
			std::vector<int> classIds, indices;
			vector<float> confidences;
			for (size_t i = 0; i < detectionMat.size(); ++i)
			{
				float* data = (float*)detectionMat[i].data;
				for (int j = 0; j < detectionMat[i].rows; ++j, data += detectionMat[i].cols)
				{
					Mat scores = detectionMat[i].row(j).colRange(5, detectionMat[i].cols);
					cv::Point classIdPoint;
					double confidence;
					minMaxLoc(scores, 0, &confidence, 0, &classIdPoint);
					int centerX = (int)(data[0] * frame.cols);
					int centerY = (int)(data[1] * frame.rows);
					int width = (int)(data[2] * frame.cols);
					int height = (int)(data[3] * frame.rows);
					int left = centerX - width / 2;
					int top = centerY - height / 2;

					boxes.push_back(Rect(left, top, width, height));
					confidences.push_back((float)confidence);
					classIds.push_back(classIdPoint.x);
				}
			}

			// perform NMS
			NMSBoxes(boxes, confidences, confidenceThreshold, NMSThreshold, indices);

			// Save result using "Data" structure
			List<Data^>^ results = gcnew List<Data^>();
			for (size_t i = 0; i < indices.size(); ++i) {
				int classIdPoint_x = classIds[indices[i]];
				Rect box = boxes[indices[i]];
				int left = box.x;
				int top = box.y;
				int width = box.width;
				int height = box.height;
				Data^ d = gcnew Data(_names[classIdPoint_x], classIdPoint_x, confidences[indices[i]], left, top, width, height);
				results->Add(d);
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

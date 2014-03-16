About the Program

This program uses the technique of Haar training to train the program and create the vector xml for recognition. Using EmguCV and OpenCV in C#.NET to analyze images bit by bit. The program uses contour analysis to determine if there are objects in the image similar to what was learned in through Haar training

The performance of the project was improved using MPI.NET in C#.NET. This was executed in a Windows HPC Pack 2008 R2 environment to run the program in parallel across all of the processor's cores.


Technologies Used

1. MPI.NET
2. Windows HPC Pack 2008 R2
3. Contour analysis
4. Haar training
5. EmguCV Windows Universal GPU 2.4.9.1847
6. OpenCV


Installing Dependencies

1. .Net 4.5 (dotnetfx45_full_setup.exe)
2. Microsoft HPC Pack 2008 R2; HPC Pack 2008 R2 with SP4
3. msmpi; mpi_x64/x86.msi (if not you wont get mpiexec.exe)
4. MPI.NET; Runtime.msi (if not it will require MPI.NET 1.0.0)
5. VS c++; vcredist_x86/x64  ( for emgu.cv)
5. emgucv-windows-universal-gpu 2.4.9.1847; OpenCV_Emgu (if not it will threw emgu.cv) 

If you experience errors it will most likely be because of evextern.dll. Examine the file with DependencyWalker to find out the file it is expecting and move this .dll to ./Microsoft HPC Pack 2008 R2/bin. Reference: http://www.emgu.com/wiki/index.php/Download_And_Installation#The_type_initializer_for_.27Emgu.CV.CvInvoke.27_threw_an_exception.


Dependency Checklist
cudart64_42_9.dll, 
cvextern.dll, 
npp64_42_9.dll, 
opencv_calib3dXXX.dll, 
opencv_contribXXX.dll, 
opencv_coreXXX.dll, 
opencv_features2dXXX.dll, 
opencv_flannXXX.dll, 
opencv_highguiXXX.dll, 
opencv_imgprocXXX.dll, 
opencv_legacyXXX.dll, 
opencv_mlXXX.dll, 
opencv_nonfreXXX.dll, 
opencv_objectdetectXXX.dll, 
opencv_videoXXX.dll, 
where XXX is the OpenCV version number. 


Executing the Program

From the Program Files\Microsoft HPC Pack 2008 Rd\Bin directory execute: mpiexec -n [number of cores] HumerusDetectBroken.exe

example:

mpiexec -n 4 HumerusDetectBroken.exe
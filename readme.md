Executing the program

1. Install all dependencies.
2. From the Program Files\Microsoft HPC Pack 2008 Rd\Bin directory execute:
mpiexec -n [number of cores] HumerusDetectBroken.exe

example:

mpiexec -n 4 HumerusDetectBroken.exe


Technologies used

1. MPI.NET
2. Windows HPC Pack 2008 R2
3. Contour analysis
4. Haar training
5. EmguCV Windows Universal GPU 2.4.9.1847
6. OpenCV


About the program

This program uses the technique of Haar training to train the program and create the vector xml for recognition. Using EmguCV and OpenCV in C#.NET to analyze images bit by bit. The program uses contour analysis to determine if there are objects in the image similar to what was learned in through Haar training

The performance of the project was improved using MPI.NET in C#.NET. This was executed in a Windows HPC Pack 2008 R2 environment to run the program in parallel across all of the processor's cores.
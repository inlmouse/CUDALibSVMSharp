CUDALibSVMSharp
====
**.NET Framework Based LIBSVM Accelerated with GPU using CUDA **

CUDALibSVMSharp is a modification of the [original LIBSVM](http://www.csie.ntu.edu.tw/~cjlin/libsvm/) that exploits the CUDA framework to significantly reduce processing time while producing identical results. The functionality and interface of LIBSVM remains the same. The modifications were done in the kernel computation, that is now performed using the GPU. And it has been packaged as a standalone .NET wrapper using C++/CLI (for the .NET interface) and includes the modified C code of libsvm.



###FEATURES

Mode Supported

    C-SVC classification with RBF kernel

Functionality / User interface

    Same as LIBSVM

###Dependency

    LIBSVM prerequisites
    NVIDIA Graphics card with CUDA support
    CUDA Toolkit 7.5
    Microsoft Visual Studio 2015(for compiling LibSVM.NET version)
	
###PERFORMANCE COMPARISON

To showcase the performance gain using the CUDALibSVMSharp we present an example run.

PC Setup

    Intel Xeon cpu E5-1603 v3
    Nvidia Quadro K2200
    16GB of DDR4 RAM
    Windows 10 64-bit OS
    
Classification parameters

    c-svc
    RBF kernel
    Parameter optimization using the easy.py script provided by LIBSVM.
    4 local workers
![Diagram](https://github.com/inlmouse/CUDALibSVMSharp/blob/master/GPULIBSVM-comparison.jpg)

Discussion

    CUDALibSVMSharp gives a performance gain depending on the size of input data set. 
    This gain is increasing dramatically with the size of the dataset.
    Please take into consideration input data size limitations that can occur from the memory 
    capacity of the graphics card that is used.    
    
###PUBLICATION

If you use this code for any kind of publication please include the following references:

```
@inproceedings{athanasopoulos2011gpu,
  title={GPU acceleration for support vector machines},
  author={Athanasopoulos, Andreas and Dimou, Anastasios and Mezaris, Vasileios and Kompatsiaris, Ioannis},
  booktitle={WIAMIS 2011: 12th International Workshop on Image Analysis for Multimedia Interactive Services, Delft, The Netherlands, April 13-15, 2011},
  year={2011},
  organization={TU Delft; EWI; MM; PRB}
}
```

```
@techreport{tr-vrvis-20150609,
     title = {{Implementation of Semantic Texton Forests for Image Categorization and Segmentation}},
     author = {Attila Szabo and Stefan Maierhofer},
     year = {2015},
     institution = {VRVis Research Center},
     month = {06},
     url = {http://download.vrvis.at/acquisition/tr/VRVis_TR_szabo_maierhofer_semantictextonforests.pdf}
}
```

###License
=======

MIT (http://opensource.org/licenses/MIT)

Authors: inlmouse(inlmouse@hotmail.com)
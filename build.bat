set UNITY="C:/Program Files/Unity/Hub/Editor/2019.2.12f1/Editor/Unity.exe"
set BUILD_ROOT=C:/BuiltUnity/
set LINUX=LatencyTest-Linux
set LINUX_FULL=LatencyTest-Linux/LatencyTest
set MAC=LatencyTest-Mac
set WIN=LatencyTest-Win
set WIN_FULL=LatencyTest-Win/LatencyTest

%UNITY% -batchmode -quit -logFile build-linux.log -buildLinux64Player %BUILD_ROOT%%LINUX_FULL%
%UNITY% -batchmode -quit -logFile build-osx.log -buildOSXUniversalPlayer  %BUILD_ROOT%%MAC%
%UNITY% -batchmode -quit -logFile build-win.log -buildWindows64Player  %BUILD_ROOT%%WIN_FULL%

rem Time to zip!

7z a %BUILD_ROOT%%LINUX%.zip %BUILD_ROOT%%LINUX%\
7z a %BUILD_ROOT%%MAC%.zip %BUILD_ROOT%%MAC%.app\
7z a %BUILD_ROOT%%WIN%.zip %BUILD_ROOT%%WIN%\
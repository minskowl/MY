REM RUN FROM VS.NET PROMPT
@PAUSE
REM UNREGISTER COM OBJECTS, THEN REGISTER
REM FOR INSTALL IN GLOBAL ASSEMBLY, SIGN 
regasm /u bin\debug\FileDescriptor.exe
REM gacutil /u FileDescriptor.exe
regasm bin\debug\FileDescriptor.exe
REM gacutil /i bin\debug\FileDescriptor.exe
REM REGISTER AS SHELL OBJECTS IN REGISTRY
REM FOR Active Directory registration, see MS-AD documentation!
regApproved.reg
regBindToEXE.reg




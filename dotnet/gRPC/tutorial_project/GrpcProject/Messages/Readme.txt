protoc 
\\trzeba wskazac w jakim folderze sa zamieszczone wszystkie wiadomosci proto
-I C:\Repo\tutorials\master\dotnet\gRPC\tutorial_project\pb  
\\trzeba wskazac:
--csharp_out 
	\\gdzie jako pierwsze maja sie wygenerowac pliki cs
	C:\Repo\tutorials\master\dotnet\gRPC\tutorial_project\GrpcProject\Messages\ 
	\\wszystkie pliki proto z ktorych zostanie wygenerowany kod c#
	C:\Repo\tutorials\master\dotnet\gRPC\tutorial_project\pb\messages.proto 
\\trzeba wskazac w jakim miejscu ma sie wygenerowac kod c# z plikow proto
--grpc_out C:\Repo\tutorials\master\dotnet\gRPC\tutorial_project\GrpcProject\Messages\ 
\\wskazanie pluginu do generowania kodu serwera z plikow proto
--plugin=protoc-gen-grpc=C:\Users\kmara\.nuget\packages\grpc.tools\2.36.4\tools\windows_x86\grpc_csharp_plugin.exe
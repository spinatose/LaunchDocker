# LaunchDocker
C# console app to LaunchDocker containers

### Notes
* If there are any quoted args in the docker command that you are passing in, you must enclose your arguments in single quotes to preserve them in the passed in command line argument:
```
dotnet run '--network=host -it -p 8999:8000 -v ~/dev/simhosp:/config/pathways eu.gcr.io/simhospital-images/simhospital:latest sh -c "health/simulator -pathways_dir /config/pathways -output mllp -mllp_destination host.docker.internal:7337"'
```

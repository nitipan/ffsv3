rmdir /s /q dist
devenv "ffs.sln" /build Debug /project "ffs.api\ffs.api.csproj"

cd ffs.web
call ng build --prod

pause
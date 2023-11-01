@echo off

start cmd /k "cd backend && dotnet watch && pause"

cd frontend

IF NOT EXIST node_modules (
	echo Rodando npm install... && npm install
)

ng serve && pause && pause && pause
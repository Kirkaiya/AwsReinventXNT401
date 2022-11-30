#!/bin/bash
# Install .NET 6.0 SDK for ARM64
sudo apt-get update -y
curl -O https://download.visualstudio.microsoft.com/download/pr/adcd9310-5072-4179-9b8b-16563b897995/15a7595966f488c74909e4a9273c0e24/dotnet-sdk-6.0.100-linux-arm64.tar.gz
mkdir /usr/bin/dotnet
sudo tar -zxvf dotnet-sdk-6.0.100-linux-arm64.tar.gz -C /usr/bin/dotnet
sudo ln -s /usr/bin/dotnet/dotnet /usr/bin/dotnet
sudo sh -c 'echo "export DOTNET_ROOT=/usr/bin/dotnet" >> /etc/environment'
sudo sh -c 'echo "export PATH=$PATH:$DOTNET_ROOT" >> /etc/environment'

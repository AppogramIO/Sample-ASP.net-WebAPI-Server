# Appogram Sample With ASP.net/C#

this repository contain a very simple server that written in ASP.net/C# with WebAPI technology.

# Installation
For using this sample just download the repository and then open it in Visual Studio and then run it. for testing codes we use `Visual Studio 2015` and `.NET Framework 4` so we reacommend you to use these versions.

***some notes***
 By default server run on `localhost:{someport}` and ready to use.then you can use the address of server in [Appogram Studio](https://studio.appogram.io)

# Use in Appogram Studio 
After you run the server you need to add the url of server in  [Appogram Studio](https://studio.appogram.io) but first of all be sure that you download the last version of [Appo Template](https://github.com/AppogramIO/Sample-Appogram-Appo) and then import it to versions. after that go to `Designer` and on top of page select `Variables`.Click on `Variables` button after a modal opend edit the `server` variable and replace it with this server url.

**Important Note 1**
The server run on `localhost` and port `(somer random port like 80)` but as you know if you use this url in you app it cause a problem in android emulator or other devices. so instead of using `localhost`  use your **ip address** for example if your app address is `192.168.1.10` instead of using `localhost:80` use `192.168.1.10:80` for `server` variable in Appogram Studio.

**Important Note 2**
This is very important that after you import the version in Appogram Studio then save it. for this go to `Designer` and then click on `Save and Update` button.

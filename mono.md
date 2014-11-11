# Vorbereitung

Raspberry PI Model B+ besorgen ;)

Raspian über eine SD Karte mit min. 4GB installieren und ggf. die CPU übertakten

Raspian updaten:

	sudo apt-get update
	sudo apt-get upgrade

# Mono Installieren

	$ sudo apt-get install mono-complete
	$ sudo apt-get install mono-csharp-shell

## Mono testen

Die C# Shell aufrufen:

	$ csharp

In der Bash nun einen autput auf die Console versuchen:<br>

	csharp> Console.WriteLine("Hello World from Mono ({0})", Environment.OSVersion);

Das Resultat sollte folgendes sein:

	Hello World from Mono (Unix 3.6.11.0)

Hin und wieder soll es (bei der falschen Raspian Version) zu rechenfehlern kommen. Wir testen dass auch:<br>

	csharp> Math.Pow(2, 4);

Ist das Resultat nun 16 stimmt alles ;)


# Das erste Programm

Einen editor der Wahl installieren und nutzen, z. B. Emacs:

	sudo apt-get install emacs

Wir erzeugen nun ein neues Verzeichnis, wechseln in dieses und erstellen eine neue Datei mit dem Namen "HelloWorld.cs":

	mkdir HelloWorld
	cd HelloWorld
	emacs HelloWorld.cs
	
Wir schreiben nun folgenden Code in dne Editor:

	using System;
	public class HelloWorld`<br>
	{
		public static void Main()
		{
			Console.WriteLine("Hello World!");
		}
	}
	
Anschließend wird die Dateo kompiliert:

	gmcs HelloWorld.cs

Das ergebnis können wir nun aufrufen:

	mono HelloWorld.exe

Damit haben wir unser erstes C# Programm unter Mono auf dem Raspian am laufen :)

# Owin / Katana

	Zur Errinnerung Owin / katana

Nutzung von MonoDevelop / XamarinStudio mit Mono unter Windows

Deployment per GIT oder USB-Stick

Ich wechsle nun in das Solution-Verzeichnis, baue nun die Solution und wechsle nun in das BIN-Vereichnis. dort rufe ich die OwinSelfHost auf und kann nun den Web-Dienst über den Browser aufrufen.

	> sudo apt-get install git ruby rake
	> sudo gem install albacore

oder alternativ

	> xbuild


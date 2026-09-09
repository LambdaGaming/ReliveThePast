# Relive The Past
Relive The Past is an EXILED plugin for SCP:SL that respawns players as a Scientist or Class D after a set period of time if they die early in the round. It's intended to be used on servers with low player counts to prevent rounds from ending early. There's also an option to have Class Ds spawn with a keycard after a certain amount of time.

Respawning is disabled if any of the following conditions are met:
- Light Containment decontamination has started, or is less than 30 seconds away from starting
- Warhead has detonated or countdown is active
- All MTF and Chaos respawn waves have been paused

# Building
 The project files are intended to be built using either the command line or VSCode with the C# Dev Tools extension, but Visual Studio should work too. You will need to have the SCP:SL dedicated server and .NET SDK 8 or above installed. You will also need to change the reference paths in the .csproj file to the location of your dedicated server.

# Contributing
 Contributions are welcome! Please read through the [guidelines](https://lambdagaming.github.io/guides/contributing) before submitting an issue or pull request.

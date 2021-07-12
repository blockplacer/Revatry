# Revatry
An alternative web framework for .NET

Some parts of this project is async performance not being tested yet.

Planning to do some stress test.



I done this very quickly there might be bugs please make issue request if you seen a bug,
try improving code by pull requests.




It does supports

- Sessions
- Pages and virtual pages
- Fleck Compatibile
- Templating using regex
- Html appending on every page so like navbars/sidebars could be easily done
- A custom database system 

Planned:

- Html Compression (server side


Dependencies:
- Fleck
- HttpListener (Only available on windows 8 and later operating systems due only win 11, win 10, win 8 (including server os based on these osses) supports http.sys)
- Newtonsoft.JSON

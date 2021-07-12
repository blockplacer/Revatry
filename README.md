# Revatry
An alternative web framework for .NET

Some parts of this project is async performance not being tested yet.

Planning to do some stress test.

It does supports

- Sessions (semi, not tested)
- Pages and virtual pages
- Fleck Compatibile

Planned:

- Templating using regex
- Html Compression (server side)
- Html appending on every page so like navbars/sidebars could be easily done
- A custom database system 

Dependencies:
- Fleck
- HttpListener (Only available on windows 8 and later operating systems due only win 11, win 10, win 8 (including server os based on these osses) supports http.sys)
- Newtonsoft.JSON

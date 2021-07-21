# Revatry
An alternative web framework for .NET

Info:
Pages runs on different tasks


I done this very quickly there might be bugs please make issue request if you seen a bug,
try improving code by pull requests.




It does supports

- Sessions
- Pages and virtual pages
- Fleck Compatibile
- Templating using regex
- Html appending on every page so like navbars/sidebars could be easily done
- A custom database system 
- Supports GET,POST,PUT,DELETE (for rest apis)
- Time conversion for human readable format

Planned:

- Html Compression (server side)
- Sql Parser ( sql injection proof, forced prepared statements, database currently uses linq for some stuff )
- Custom Http/https server for other operating systems


Dependencies:
- Fleck
- HttpListener (Only available on windows 8 and later operating systems due only win 11, win 10, win 8 (including server os based on these osses) supports http.sys)
- Newtonsoft.JSON

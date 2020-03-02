# CodeCommenter for Visual Studio

A utility to facilitate the creation and maintenance of comment structures in software code.

### Paragraph Comment - Ctrl-K, Ctrl-C

Builds a comment block for insertion into an editor file. The input text can
be in a layout format or the text can be contiguous and unformatted. The
text will be formatted into a block and surrounded with comment delimiters.
The delimiters used is dependent on the file name extension or the current 
comment style. For a C program, for instance, '/*' style delimiters are used.

When the selected text is loaded into the object, it is cleaned of all
delimiters before any formatting takes place so it is not necessary to
remove old comment delimiters before selecting the text to be formatted.

When a block of text is to be formatted, the maximum width and number of
lines requested is specified in the call to the Format() method. If the
number of lines is not specified, the block will be optimized for line width
such that any dangling last lines will not be much smaller than the other
lines in the block. The Maximum Line Width overrides all other
considerations. So, for 30 character line width specified and 3 lines
requested with 220 characters of text selected, there will be many more than
3 lines, each no more than 30 characters.

#### Examples

    <---- Selected Block ---->     <------ Commented Block ------>
                                   '*----------------------------*
    This is a formatted block:     '* This is a formatted block: *
      1) 1st bullet.               '*  1) 1st bullet.            *
      2) 2nd bullet.               '*  2) 2nd bullet.            *
                                   '*----------------------------*

    <----------------------------- Selected Text ---------------------->
    This is non-formatted, contiguous text which will be formated into a 
    block of text according to the lines and maximum width specified.

    <------------------ Commented Block ------------------->
    '*-----------------------------------------------------*
    '* This is non-formatted, contiguous text which will   *
    '* will be formated into a block of text according     *
    '* to the number of lines and maximum width specified. *
    '*-----------------------------------------------------*




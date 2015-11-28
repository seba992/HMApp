%% wersja 3.0 -- nowy uk³ad stron tyt.
%% wersja 2.3
%% wersja 2.2 -- poprawka -- usuni©cie kropki po numerze rozdziaˆu
%%               przy komendzie "\rozdzialy" (8 I 2004r.)
%% wersja 2.3 -- zmiana koncepcji kropki aby uzyskac zgodnosc z amsmath 
%% wersja 2.4 -- dodane komendy \wnioski \rysunki 
%%               \sectionRysunki \subsectionRysunki
%%              oraz inne zachowanie pustego \caption{}
%%              (10.05.2005r.)
%% wersja 2.5 -- kierunek z du¿ej litery, praca licencjacka
%%               oraz komenda pozwalaj¹ca na rozszerzani komend typu \rysunki, \wnioski.. 
%%              \dodatkowo{nazwa}
%%           
%% wersja 2.6 -- inaczej prace 
%%           
%%            
%% This is file `article.cls',
%% Zmieniony na dyplomMS.cls --- dokonano poprawek
%% zwiazanych z uzyciem stylu do prac dyplomowych
%% 
%%
%%
%% \CharacterTable
%%  {Upper-case    \A\B\C\D\E\F\G\H\I\J\K\L\M\N\O\P\Q\R\S\T\U\V\W\X\Y\Z
%%   Lower-case    \a\b\c\d\e\f\g\h\i\j\k\l\m\n\o\p\q\r\s\t\u\v\w\x\y\z
%%   Digits        \0\1\2\3\4\5\6\7\8\9
%%   Exclamation   \!     Double quote  \"     Hash (number) \#
%%   Dollar        \$     Percent       \%     Ampersand     \&
%%   Acute accent  \'     Left paren    \(     Right paren   \)
%%   Asterisk      \*     Plus          \+     Comma         \,
%%   Minus         \-     Point         \.     Solidus       \/
%%   Colon         \:     Semicolon     \;     Less than     \<
%%   Equals        \=     Greater than  \>     Question mark \?
%%   Commercial at \@     Left bracket  \[     Backslash     \\
%%   Right bracket \]     Circumflex    \^     Underscore    \_
%%   Grave accent  \`     Left brace    \{     Vertical bar  \|
%%   Right brace   \}     Tilde         \~}
\NeedsTeXFormat{LaTeX2e}[1995/12/01]
\ProvidesClass{projektInzynierskiMS}
              [2005/05/10 v2.4
 nonStandard LaTeX document class]
\newcommand\@ptsize{}
\newif\if@restonecol
\newif\if@titlepage
\@titlepagefalse

%
% PRZEDEFINIOWANIE !!!!
%
%

\def\refstepcounter#1{\stepcounter{#1}%
    \protected@edef\@currentlabel
       {\csname p@#1\endcsname\csname the#1\endcsname\protect}%
}


\def\backslashDYPMF{\char92\relax}
\def\NawOtwDYPMF{\char123\relax}
\def\NawZamDYPMF{\char125\relax}

\def\WyjasnienieRodz{\hfill}
\def\WyjasnienieTyt{\hfill}
\def\WyjasnienieAut{\hfill}
\def\WyjasnienieProm{\hfill}


% \newif\if@magister
% \@magisterfalse
% 3XI 2000 zmienilem zdanie.


%%%--{\Huge\bfseries NIEZDEFINIOWANY\\
%%%--Musisz da\'c jedn\k a z komend:\\
%%%--{\tt \backslashDYPMF PracaMagisterska }\\ {\tt \backslashDYPMF PracaInzynierska}\\ w preambule}}

%\if@compatibility\else
%\DeclareOption{a4paper}
%   {\setlength\paperheight {297mm}%
%    \setlength\paperwidth  {210mm}}
%\DeclareOption{a5paper}
%   {\setlength\paperheight {210mm}%
%    \setlength\paperwidth  {148mm}}
%\DeclareOption{b5paper}
%   {\setlength\paperheight {250mm}%
%    \setlength\paperwidth  {176mm}}
%\DeclareOption{letterpaper}
%   {\setlength\paperheight {11in}%
%    \setlength\paperwidth  {8.5in}}
%\DeclareOption{legalpaper}
%   {\setlength\paperheight {14in}%
%    \setlength\paperwidth  {8.5in}}
%\DeclareOption{executivepaper}
%   {\setlength\paperheight {10.5in}%
%    \setlength\paperwidth  {7.25in}}
%\DeclareOption{landscape}
%   {\setlength\@tempdima   {\paperheight}%
%    \setlength\paperheight {\paperwidth}%
%    \setlength\paperwidth  {\@tempdima}}
%\fi
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%
%% Nowe opcja mgr, mag oraz inz
%%
%% dodane 19.02.2010: opcja praca licencjacka, opcje: lic oraz l lub L
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
\let\rodzajKodow\relax
\DeclareOption{dos}     {\def\rodzajKodow{\usepackage[cp852]{inputenc}}}
\DeclareOption{win}     {\def\rodzajKodow{\usepackage[cp1250]{inputenc}}}




\if@compatibility
  \renewcommand\@ptsize{0}
\else
\DeclareOption{10pt}{\renewcommand\@ptsize{0}}
\fi
\DeclareOption{11pt}{\renewcommand\@ptsize{1}}
\DeclareOption{12pt}{\renewcommand\@ptsize{2}}
\if@compatibility\else
%\DeclareOption{oneside}{\@twosidefalse \@mparswitchfalse}
\fi
\DeclareOption{twoside}{\@twosidetrue  \@mparswitchtrue}
\DeclareOption{draft}{\setlength\overfullrule{5pt}}
\if@compatibility\else
\DeclareOption{final}{\setlength\overfullrule{0pt}}
\fi
\DeclareOption{titlepage}{\@titlepagetrue}
\if@compatibility\else
\DeclareOption{notitlepage}{\@titlepagefalse}
\fi
\if@compatibility\else
\DeclareOption{onecolumn}{\@twocolumnfalse}
\fi
\DeclareOption{twocolumn}{\@twocolumntrue}

\DeclareOption{leqno}{\input{leqno.clo}}
\DeclareOption{fleqn}{\input{fleqn.clo}}
\DeclareOption{openbib}{%
  \AtEndOfPackage{%
   \renewcommand\@openbib@code{%
      \advance\leftmargin\bibindent
      \itemindent -\bibindent
      \listparindent \itemindent
      \parsep \z@
      }%
   \renewcommand\newblock{\par}}%
}
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Zmiana
%     10pt na 12pt oraz letterpaper na a4paper
%   wykonana 21 X 2000r. by Marek \.Zabka
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% \ExecuteOptions{letterpaper,10pt,oneside,onecolumn,final}
%
\ExecuteOptions{a4paper,12pt,twoside,onecolumn,final}
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
\ProcessOptions
\input{size1\@ptsize.clo}
\setlength\lineskip{1\p@}
\setlength\normallineskip{1\p@}
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% Zmiana
% \renewcommand\baselinestretch{}
%   wykonana 21 X 2000r. by Marek \.Zabka
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%  \renewcommand\baselinestretch{}
%
\renewcommand\baselinestretch{1.2}
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
\setlength\parskip{0\p@ \@plus \p@}
\@lowpenalty   51
\@medpenalty  151
\@highpenalty 301
\setcounter{topnumber}{2}
\renewcommand\topfraction{.7}
\setcounter{bottomnumber}{1}
\renewcommand\bottomfraction{.3}
\setcounter{totalnumber}{3}
\renewcommand\textfraction{.2}
\renewcommand\floatpagefraction{.5}
\setcounter{dbltopnumber}{2}
\renewcommand\dbltopfraction{.7}
\renewcommand\dblfloatpagefraction{.5}
\if@twoside
  \def\ps@headings{%
      \let\@oddfoot\@empty\let\@evenfoot\@empty
      \def\@evenhead{\thepage\hfil\slshape\leftmark}%
      \def\@oddhead{{\slshape\rightmark}\hfil\thepage}%
      \let\@mkboth\markboth
    \def\sectionmark##1{%
      \markboth {\MakeUppercase{%
        \ifnum \c@secnumdepth >\z@
          \thesection\quad
        \fi
        ##1}}{}}%
    \def\subsectionmark##1{%
      \markright {%
        \ifnum \c@secnumdepth >\@ne
          \thesubsection\quad
        \fi
        ##1}}}
\else
  \def\ps@headings{%
    \let\@oddfoot\@empty
    \def\@oddhead{{\slshape\rightmark}\hfil\thepage}%
    \let\@mkboth\markboth
    \def\sectionmark##1{%
      \markright {\MakeUppercase{%
        \ifnum \c@secnumdepth >\m@ne
          \thesection\quad
        \fi
        ##1}}}}
\fi

\def\ps@myheadings{%
    \let\@oddfoot\@empty\let\@evenfoot\@empty
    \def\@evenhead{\thepage\hfil\slshape\leftmark}%
    \def\@oddhead{{\slshape\rightmark}\hfil\thepage}%
    \let\@mkboth\@gobbletwo
    \let\sectionmark\@gobble
    \let\subsectionmark\@gobble
    }
  \if@titlepage
  \newcommand\maketitle{\begin{titlepage}%
  \let\footnotesize\small
  \let\footnoterule\relax
  \let \footnote \thanks
  \null\vfil
  \vskip 60\p@
  \begin{center}%
    {\LARGE \@title \par}%
    \vskip 3em%
    {\large
     \lineskip .75em%
      \begin{tabular}[t]{c}%
        \@author
      \end{tabular}\par}%
      \vskip 1.5em%
    {\large \@date \par}%       % Set date in \large size.
  \end{center}\par
  \@thanks
  \vfil\null
  \end{titlepage}%
  \setcounter{footnote}{0}%
  \global\let\thanks\relax
  \global\let\maketitle\relax
  \global\let\@thanks\@empty
  \global\let\@author\@empty
  \global\let\@date\@empty
  \global\let\@title\@empty
  \global\let\title\relax
  \global\let\author\relax
  \global\let\date\relax
  \global\let\and\relax
}
\else
\newcommand\maketitle{\par
  \begingroup
    \renewcommand\thefootnote{\@fnsymbol\c@footnote}%
    \def\@makefnmark{\rlap{\@textsuperscript{\normalfont\@thefnmark}}}%
    \long\def\@makefntext##1{\parindent 1em\noindent
            \hb@xt@1.8em{%
                \hss\@textsuperscript{\normalfont\@thefnmark}}##1}%
    \if@twocolumn
      \ifnum \col@number=\@ne
        \@maketitle
      \else
        \twocolumn[\@maketitle]%
      \fi
    \else
      \newpage
      \global\@topnum\z@   % Prevents figures from going at top of page.
      \@maketitle
    \fi
    \thispagestyle{plain}\@thanks
  \endgroup
  \setcounter{footnote}{0}%
  \global\let\thanks\relax
  \global\let\maketitle\relax
  \global\let\@maketitle\relax
  \global\let\@thanks\@empty
  \global\let\@author\@empty
  \global\let\@date\@empty
  \global\let\@title\@empty
  \global\let\title\relax
  \global\let\author\relax
  \global\let\date\relax
  \global\let\and\relax
}
\def\@maketitle{%
  \newpage
  \null
  \vskip 2em%
  \begin{center}%
  \let \footnote \thanks
    {\LARGE \@title \par}%
    \vskip 1.5em%
    {\large
      \lineskip .5em%
      \begin{tabular}[t]{c}%
        \@author
      \end{tabular}\par}%
    \vskip 1em%
    {\large \@date}%
  \end{center}%
  \par
  \vskip 1.5em}
\fi
\setcounter{secnumdepth}{3}
\newcounter {part}
\newcounter {section}
\newcounter {subsection}[section]
\newcounter {subsubsection}[subsection]
\newcounter {paragraph}[subsubsection]
\newcounter {subparagraph}[paragraph]
\renewcommand \thepart {\@Roman\c@part}
\renewcommand \thesection {\@arabic\c@section}
\renewcommand\thesubsection   {\thesection.\@arabic\c@subsection}
\renewcommand\thesubsubsection{\thesubsection .\@arabic\c@subsubsection}
\renewcommand\theparagraph    {\thesubsubsection.\@arabic\c@paragraph}
\renewcommand\thesubparagraph {\theparagraph.\@arabic\c@subparagraph}
\newcommand\part{%
   \if@noskipsec \leavevmode \fi
   \par
   \addvspace{4ex}%
   \@afterindentfalse
   \secdef\@part\@spart}

\def\@part[#1]#2{%
    \ifnum \c@secnumdepth >\m@ne
      \refstepcounter{part}%
      \addcontentsline{toc}{part}{\thepart\hspace{1em}#1}%
    \else
      \addcontentsline{toc}{part}{#1}%
    \fi
    {\parindent \z@ \raggedright
     \interlinepenalty \@M
     \normalfont
     \ifnum \c@secnumdepth >\m@ne
       \Large\bfseries \partname~\thepart
       \par\nobreak
     \fi
     \huge \bfseries #2%
     \markboth{}{}\par}%
    \nobreak
    \vskip 3ex
    \@afterheading}
\def\@spart#1{%
    {\parindent \z@ \raggedright
     \interlinepenalty \@M
     \normalfont
     \huge \bfseries #1\par}%
     \nobreak
     \vskip 3ex
     \@afterheading}
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%  Zmiana                                                 %%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% \newcommand\section{\@startsection {section}{1}{\z@}%
%                                    {-3.5ex \@plus -1ex \@minus -.2ex}%
%                                    {2.3ex \@plus.2ex}%
%                                    {\normalfont\Large\bfseries}}
% \newcommand\subsection{\@startsection{subsection}{2}{\z@}%
%                                      {-3.25ex\@plus -1ex \@minus -.2ex}%
%                                      {1.5ex \@plus .2ex}%
%                                      {\normalfont\large\bfseries}}
% \newcommand\subsubsection{\@startsection{subsubsection}{3}{\z@}%
%                                      {-3.25ex\@plus -1ex \@minus -.2ex}%
%                                      {1.5ex \@plus .2ex}%
%                                      {\normalfont\normalsize\bfseries}}
% \newcommand\paragraph{\@startsection{paragraph}{4}{\z@}%
%                                     {3.25ex \@plus1ex \@minus.2ex}%
%                                     {-1em}%
%                                     {\normalfont\normalsize\bfseries}}
% \newcommand\subparagraph{\@startsection{subparagraph}{5}{\parindent}%
%                                        {3.25ex \@plus1ex \@minus .2ex}%
%                                        {-1em}%
%                                       {\normalfont\normalsize\bfseries}}
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
\newcommand\section{\@startsection {section}{1}{\z@}%
                                   {3.5ex \@plus 1ex \@minus .2ex}%
                                   {10.3ex \@plus 1.2ex}%
                                   {\normalfont\LARGE\bfseries\sffamily}}
\newcommand\subsection{\@startsection{subsection}{2}{\z@}%
                                     {3.25ex\@plus 1ex \@minus .2ex}%
                                     {2.5ex \@plus .6ex}%
                                     {\normalfont\large\bfseries\sffamily}}
\newcommand\subsubsection{\@startsection{subsubsection}{3}{\z@}%
                                     {2.25ex\@plus 1ex \@minus .2ex}%
                                     {1.0ex \@plus .3ex}%
                                     {\normalfont\normalsize\bfseries\sffamily}}
\newcommand\paragraph{\@startsection{paragraph}{4}{\z@}%
                                    {3.25ex \@plus1ex \@minus.2ex}%
                                    {-1em}%
                                    {\normalfont\normalsize\bfseries}}
\newcommand\subparagraph{\@startsection{subparagraph}{5}{\parindent}%
                                       {3.25ex \@plus1ex \@minus .2ex}%
                                       {-1em}%
                                      {\normalfont\normalsize\bfseries}}
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
\let\subsectionXX=\subsection
\def\subsection{\par\PrzedSubSection\PrzedSubSectionx
\vskip0pt plus4ex \goodbreak\vskip0pt plus-4ex\relax
\subsectionXX}
\let\subsubsectionXX=\subsubsection
\def\subsubsection{\par
\vskip0pt plus4ex \goodbreak\vskip0pt plus-4ex\relax
\subsubsectionXX}

\let\sectionXX=\section
\def\section{\@ifnextchar*{\sectionAA}{\sectionBB}}

\def\sectionAA{\newoddpage 
      \thispagestyle{empty}%
       \sectionXX
       }

\def\sectionBB{\newoddpage
     \PrzedSection
     \PrzedSectionx
      \thispagestyle{empty}%
       \sectionXX
       }

\if@twocolumn
  \setlength\leftmargini  {2em}
\else
  \setlength\leftmargini  {2.5em}
\fi
\leftmargin  \leftmargini
\setlength\leftmarginii  {2.2em}
\setlength\leftmarginiii {1.87em}
\setlength\leftmarginiv  {1.7em}
\if@twocolumn
  \setlength\leftmarginv  {.5em}
  \setlength\leftmarginvi {.5em}
\else
  \setlength\leftmarginv  {1em}
  \setlength\leftmarginvi {1em}
\fi
\setlength  \labelsep  {.5em}
\setlength  \labelwidth{\leftmargini}
\addtolength\labelwidth{-\labelsep}
\@beginparpenalty -\@lowpenalty
\@endparpenalty   -\@lowpenalty
\@itempenalty     -\@lowpenalty
\renewcommand\theenumi{\@arabic\c@enumi}
\renewcommand\theenumii{\@alph\c@enumii}
\renewcommand\theenumiii{\@roman\c@enumiii}
\renewcommand\theenumiv{\@Alph\c@enumiv}
\newcommand\labelenumi{\theenumi.}
\newcommand\labelenumii{(\theenumii)}
\newcommand\labelenumiii{\theenumiii.}
\newcommand\labelenumiv{\theenumiv.}
\renewcommand\p@enumii{\theenumi}
\renewcommand\p@enumiii{\theenumi(\theenumii)}
\renewcommand\p@enumiv{\p@enumiii\theenumiii}
\newcommand\labelitemi{\textbullet}
\newcommand\labelitemii{\normalfont\bfseries \textendash}
\newcommand\labelitemiii{\textasteriskcentered}
\newcommand\labelitemiv{\textperiodcentered}
\newenvironment{description}
               {\list{}{\labelwidth\z@ \itemindent-\leftmargin
                        \let\makelabel\descriptionlabel}}
               {\endlist}
\newcommand*\descriptionlabel[1]{\hspace\labelsep
                                \normalfont\bfseries #1}
\if@titlepage
  \newenvironment{abstract}{%
      \titlepage
      \null\vfil
      \@beginparpenalty\@lowpenalty
      \begin{center}%
        \bfseries \abstractname
        \@endparpenalty\@M
      \end{center}}%
     {\par\vfil\null\endtitlepage}
\else
  \newenvironment{abstract}{%
      \if@twocolumn
        \section*{\abstractname}%
      \else
        \small
        \begin{center}%
          {\bfseries \abstractname\vspace{-.5em}\vspace{\z@}}%
        \end{center}%
        \quotation
      \fi}
      {\if@twocolumn\else\endquotation\fi}
\fi
\newenvironment{verse}
               {\let\\\@centercr
                \list{}{\itemsep      \z@
                        \itemindent   -1.5em%
                        \listparindent\itemindent
                        \rightmargin  \leftmargin
                        \advance\leftmargin 1.5em}%
                \item\relax}
               {\endlist}
\newenvironment{quotation}
               {\list{}{\listparindent 1.5em%
                        \itemindent    \listparindent
                        \rightmargin   \leftmargin
                        \parsep        \z@ \@plus\p@}%
                \item\relax}
               {\endlist}
\newenvironment{quote}
               {\list{}{\rightmargin\leftmargin}%
                \item\relax}
               {\endlist}
\if@compatibility
\newenvironment{titlepage}
    {%
      \if@twocolumn
        \@restonecoltrue\onecolumn
      \else
        \@restonecolfalse\newpage
      \fi
      \thispagestyle{empty}%
      \setcounter{page}\z@
    }%
    {\if@restonecol\twocolumn \else \newpage \fi
    }
\else
\newenvironment{titlepage}
    {%
      \if@twocolumn
        \@restonecoltrue\onecolumn
      \else
        \@restonecolfalse\newpage
      \fi
      \thispagestyle{empty}%
      \setcounter{page}\@ne
    }%
    {\if@restonecol\twocolumn \else \newpage \fi
     \if@twoside\else
        \setcounter{page}\@ne
     \fi
    }
\fi
\newcommand\appendix{\par
  \setcounter{section}{0}%
  \setcounter{subsection}{0}%
  \gdef\thesection{\@Alph\c@section}}
\setlength\arraycolsep{5\p@}
\setlength\tabcolsep{6\p@}
\setlength\arrayrulewidth{.4\p@}
\setlength\doublerulesep{2\p@}
\setlength\tabbingsep{\labelsep}
\skip\@mpfootins = \skip\footins
\setlength\fboxsep{3\p@}
\setlength\fboxrule{.4\p@}
\renewcommand \theequation {\@arabic\c@equation}
\newcounter{figure}
\renewcommand \thefigure {\@arabic\c@figure}
\def\fps@figure{tbp}
\def\ftype@figure{1}
\def\ext@figure{lof}
\def\fnum@figure{\figurename~\thefigure}
\newenvironment{figure}
               {\@float{figure}}
               {\end@float}
\newenvironment{figure*}
               {\@dblfloat{figure}}
               {\end@dblfloat}
\newcounter{table}
\renewcommand\thetable{\@arabic\c@table}
\def\fps@table{tbp}
\def\ftype@table{2}
\def\ext@table{lot}
\def\fnum@table{\tablename~\thetable}
\newenvironment{table}
               {\@float{table}}
               {\end@float}
\newenvironment{table*}
               {\@dblfloat{table}}
               {\end@dblfloat}
\newlength\abovecaptionskip
\newlength\belowcaptionskip
\setlength\abovecaptionskip{10\p@}
\setlength\belowcaptionskip{0\p@}
%%\long\def\@makecaption#1#2{%
%%  \vskip\abovecaptionskip
%%  \sbox\@tempboxa{#1: #2}%
%%  \ifdim \wd\@tempboxa >\hsize
%%    #1: #2\par
%%  \else
%%    \global \@minipagefalse
%%    \hb@xt@\hsize{\hfil\box\@tempboxa\hfil}%
%%  \fi
%%  \vskip\belowcaptionskip}
%%

%%
%% nowa wersja \@makecaption
%%
 \newdimen\capspecdimx  
\long\def\@makecaption#1#2{%
  \vskip\abovecaptionskip
  \setbox0=\hbox{#1: }%
 \capspecdimx=\wd0 
  \sbox\@tempboxa{#1: #2}%
  \ifdim \wd\@tempboxa >\hsize
    #1: #2\par
  \else
    \global \@minipagefalse
    \ifdim \wd\@tempboxa >\capspecdimx
    \hb@xt@\hsize{\hfil\box\@tempboxa\hfil}%
    \else
    \hb@xt@\hsize{\hfil#1.\hfil}%
    \fi
  \fi
  \vskip\belowcaptionskip}
%%
%% koniec nwej wersji
%%  
\DeclareOldFontCommand{\rm}{\normalfont\rmfamily}{\mathrm}
\DeclareOldFontCommand{\sf}{\normalfont\sffamily}{\mathsf}
\DeclareOldFontCommand{\tt}{\normalfont\ttfamily}{\mathtt}
\DeclareOldFontCommand{\bf}{\normalfont\bfseries}{\mathbf}
\DeclareOldFontCommand{\it}{\normalfont\itshape}{\mathit}
\DeclareOldFontCommand{\sl}{\normalfont\slshape}{\@nomath\sl}
\DeclareOldFontCommand{\sc}{\normalfont\scshape}{\@nomath\sc}
\DeclareRobustCommand*\cal{\@fontswitch\relax\mathcal}
\DeclareRobustCommand*\mit{\@fontswitch\relax\mathnormal}
\newcommand\@pnumwidth{1.55em}
\newcommand\@tocrmarg{2.55em}
\newcommand\@dotsep{4.5}
\setcounter{tocdepth}{3}
\newcommand\tableofcontents{%
    \section*{\contentsname
        \@mkboth{%
           \MakeUppercase\contentsname}{\MakeUppercase\contentsname}}%
    \@starttoc{toc}%
    }
\newcommand*\l@part[2]{%
  \ifnum \c@tocdepth >-2\relax
    \addpenalty\@secpenalty
    \addvspace{2.25em \@plus\p@}%
    \begingroup
      \parindent \z@ \rightskip \@pnumwidth
      \parfillskip -\@pnumwidth
      {\leavevmode
       \large \bfseries #1\hfil \hb@xt@\@pnumwidth{\hss #2}}\par
       \nobreak
       \if@compatibility
         \global\@nobreaktrue
         \everypar{\global\@nobreakfalse\everypar{}}%
      \fi
    \endgroup
  \fi}
\newcommand*\l@section[2]{%
  \ifnum \c@tocdepth >\z@
    \addpenalty\@secpenalty
    \addvspace{1.0em \@plus\p@}%
    \setlength\@tempdima{1.8em}%
    \begingroup
      \parindent \z@ \rightskip \@pnumwidth
      \parfillskip -\@pnumwidth
      \leavevmode \bfseries
      \advance\leftskip\@tempdima
      \hskip -\leftskip
      #1\nobreak\hfil \nobreak\hb@xt@\@pnumwidth{\hss #2}\par
    \endgroup
  \fi}
%% poprawki szeroko˜ci
%\newcommand*\l@subsection{\@dottedtocline{2}{1.5em}{2.3em}}
\newcommand*\l@subsection{\@dottedtocline{2}{1.5em}{2.8em}}
%\newcommand*\l@subsubsection{\@dottedtocline{3}{3.8em}{3.2em}}
\newcommand*\l@subsubsection{\@dottedtocline{3}{3.8em}{4.2em}}
\newcommand*\l@paragraph{\@dottedtocline{4}{7.0em}{4.1em}}
\newcommand*\l@subparagraph{\@dottedtocline{5}{10em}{5em}}
\newcommand\listoffigures{%
    \section*{\listfigurename
      \@mkboth{\MakeUppercase\listfigurename}%
              {\MakeUppercase\listfigurename}}%
    \@starttoc{lof}%
    }
\newcommand*\l@figure{\@dottedtocline{1}{1.5em}{2.3em}}
\newcommand\listoftables{%
    \section*{\listtablename
      \@mkboth{%
          \MakeUppercase\listtablename}{\MakeUppercase\listtablename}}%
    \@starttoc{lot}%
    }
\let\l@table\l@figure
\newdimen\bibindent
\setlength\bibindent{1.5em}
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% orginalna wersja zmieniona:
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%\newenvironment{thebibliography}[1]
%     {\section*{\refname
%        \@mkboth{\MakeUppercase\refname}{\MakeUppercase\refname}}%
%      \list{\@biblabel{\@arabic\c@enumiv}}%
%           {\settowidth\labelwidth{\@biblabel{#1}}%
%            \leftmargin\labelwidth
%            \advance\leftmargin\labelsep
%            \@openbib@code
%            \usecounter{enumiv}%
%            \let\p@enumiv\@empty
%            \renewcommand\theenumiv{\@arabic\c@enumiv}}%
%      \sloppy
%      \clubpenalty4000
%      \@clubpenalty \clubpenalty
%      \widowpenalty4000%
%      \sfcode`\.\@m}
%     {\def\@noitemerr
%       {\@latex@warning{Empty `thebibliography' environment}}%
%      \endlist}
%
%  Po zmianach:
%
\newenvironment{thebibliography}[1]
     {\section*{\refname
%%
%% wyniesione ni¿ej 30 V 2006, ¿eby dzia³o z hyperref.sty
%%     
%        \addcontentsline{toc}{section}{\refname}%
        \markboth{\MakeUppercase\refname\liniaA}{\liniaB\MakeUppercase\refname}%
        \@mkboth{\MakeUppercase\refname}{\MakeUppercase\refname}}%
         \addcontentsline{toc}{section}{\refname}%
      \list{\@biblabel{\@arabic\c@enumiv}}%
           {\settowidth\labelwidth{\@biblabel{#1}}%
            \leftmargin\labelwidth
            \advance\leftmargin\labelsep
            \@openbib@code
            \usecounter{enumiv}%
            \let\p@enumiv\@empty
            \renewcommand\theenumiv{\@arabic\c@enumiv}}%
      \sloppy
      \clubpenalty4000
      \@clubpenalty \clubpenalty
      \widowpenalty4000%
      \sfcode`\.\@m}
     {\def\@noitemerr
       {\@latex@warning{Empty `thebibliography' environment}}%
      \endlist}


%%% do test¢w \twocolumn
\def \twocolumn {%
  \clearpage
  \global\columnwidth\textwidth
  \global\advance\columnwidth-\columnsep
  \global\divide\columnwidth\tw@
  \global\hsize\columnwidth
  \global\linewidth\columnwidth
  \global\@twocolumntrue
  \global\@firstcolumntrue
  \col@number \tw@
  \@ifnextchar [\@topnewpage\@floatplacement
}



\newcommand\newblock{\hskip .11em\@plus.33em\@minus.07em}
\let\@openbib@code\@empty
\newenvironment{theindex}
               {\if@twocolumn
                  \@restonecolfalse
                \else
                  \@restonecoltrue
                \fi
                \columnseprule \z@
                \columnsep 35\p@
                \twocolumn[\section*{\indexnameY}]%
% dodane 3 X 2000
                \markboth{\MakeUppercase\indexnameX\liniaA}%
                        {\liniaB\MakeUppercase\indexnameX}%
                \@mkboth{\MakeUppercase\indexname}%
                        {\MakeUppercase\indexname}%
% zmienione na empty:
%                \thispagestyle{plain}\parindent\z@
                \thispagestyle{empty}\parindent\z@
                \parskip\z@ \@plus .3\p@\relax
                \let\item\@idxitem}
               {\if@restonecol\onecolumn\else\clearpage\fi}
\newcommand\@idxitem{\par\hangindent 40\p@}
\newcommand\subitem{\@idxitem \hspace*{20\p@}}
\newcommand\subsubitem{\@idxitem \hspace*{30\p@}}
\newcommand\indexspace{\par \vskip 10\p@ \@plus5\p@ \@minus3\p@\relax}
\renewcommand\footnoterule{%
  \kern-3\p@
  \hrule\@width.4\columnwidth
  \kern2.6\p@}
\newcommand\@makefntext[1]{%
    \parindent 1em%
    \noindent
    \hb@xt@1.8em{\hss\@makefnmark}#1}
\newcommand\contentsname{Contents}
\newcommand\listfigurename{List of Figures}
\newcommand\listtablename{List of Tables}
\newcommand\refname{References}
\newcommand\indexname{Index}
\newcommand\figurename{Figure}
\newcommand\tablename{Table}
\newcommand\partname{Part}
\newcommand\appendixname{Appendix}
\newcommand\abstractname{Abstract}
\def\today{\ifcase\month\or
  January\or February\or March\or April\or May\or June\or
  July\or August\or September\or October\or November\or December\fi
  \space\number\day, \number\year}
\setlength\columnsep{10\p@}
\setlength\columnseprule{0\p@}
\pagestyle{plain}
\pagenumbering{arabic}
\if@twoside
%%% CZY TAK CZY NIE?
  \raggedbottom
\else
  \raggedbottom
\fi
\if@twocolumn
  \twocolumn
  \sloppy
  \flushbottom
\else
  \onecolumn
\fi
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%                                                  %%
%  Dodatkowe definicje                             %%
%                                                  %%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


\def\newoddpage{\newpage\ifodd\c@page \else\hbox{} \thispagestyle{empty} \newpage\fi}


\newcommand{\fontnag}{\Large}
\newcommand{\fonttyt}{\huge\bfseries\sffamily}
\newcommand{\fontpromotor}{\large\itshape}
\newcommand{\fontautor}{\large\itshape}
\newcommand{\fontautoretyk}{\large}
\newcommand{\fontpromotoretyk}{\large}
\newcommand{\fontrodzpracy}{\Large\sffamily}
\newcommand{\fontmiasto}{\large\itshape\bfseries}

\let\newoddpagex\newoddpage

\def\newoddpagexxx{\let\newoddpage\newoddpagex }

\newif\ifJest \Jestfalse
\newif\ifJestleqno \Jestleqnofalse
\def\CzyJestCD{\ifx\CzyJestTMPA\CzyJestTMP \global\Jesttrue \fi}
\def\CzyJest#1{%\edef\ListaPlikow{\@filelist}%
\def\CzyJestTMP{#1}\@for\CzyJestTMPA:=\@filelist\do\CzyJestCD\relax}

\def\kierunek{Informatyka}
\def\kierunekMatematyka{\def\kierunek{Matematyka}}
\def\kierunekInformatyka{\def\kierunek{}}
\def\kierunekFizyka{\def\kierunek{Fizyka Techniczna}}
\def\kierunekElektronika{\def\kierunek{Elektronika i Telekomunikacja}}
\def\ustawKierunekW#1{\def\kierunek{#1}}

\def\miasto#1{\global\def\miastoname{#1}}
\miasto{Gliwice}
%% przedefiniowanie \title
\def\title#1{\gdef\@title{#1}}
\let\tytul\title
\def\promotor#1{\gdef\@promotor{#1}}
\let\prowadzacyPrace\promotor
%%% problem autor/autorzy 14.03.2011
%%%\newcounter{NrAutora}
%%%\newcounter{NrAutoraX}
%%%\setcounter{NrAutora}{0}
%%%\setcounter{NrAutoraX}{0}
%%%\def\nazwa@autorow{Autor}
%%%\let\@@aut\empty


\def\nazwa@autorow{Autor}
%%%\let\@@aut\empty
%%%\let\@author\empty
\def\chron{\noexpand\chron\noexpand}
%%%%\def\author#1{\global\advance\nr@@tora1
%%%%	\expandafter\gdef\csname @autor\the\nr@@tora\endcsname{#1}%
%%%%	\ifx\@author\empty
%%%%	\global\edef\@author{\chron\fontautor\chron\csname @autor1\endcsname}%
%%%%	\else
%%%%	\def\nazwa@autorow{Autorzy}%
%%%%	\global\edef\@author{\@author\chron\\\chron\fontautor\chron\csname @autor\the\nr@@tora\endcsname}%
%%%%	\fi
%%%%	}


\newcount\nr@@tora
\nr@@tora=0

\long\def\autor#1#2#3#4{%
\global\advance\nr@@tora1\relax
\expandafter\gdef\csname aut@@or\the\nr@@tora\endcsname{#1}%
\expandafter\gdef\csname album@@or\the\nr@@tora\endcsname{#2}%
\expandafter\gdef\csname proc@@or\the\nr@@tora\endcsname{#3}%
\expandafter\gdef\csname opaut@@or\the\nr@@tora\endcsname{#4}%
\ifnum\nr@@tora=1\relax  
     \def\nazwa@autorow{Autor}%
     \def\nazwa@autorowa{autora}%
     \def\nazwa@autorowb{Podpis autora pracy}%
     \def\dodMnogiAut##1##2{##1}%
    \else
     \def\nazwa@autorow{Autorzy}%
     \def\nazwa@autorowa{autor\'ow}%
     \def\nazwa@autorowb{Podpisy autor\'ow projektu %
     }%in¿ynierskiego}%
     \def\dodMnogiAut##1##2{##2}%
     \fi
}
\def\nazwa@autorow{}%
\def\nazwa@autorowa{}%
\def\nazwa@autorowb{}%
\def\dodMnogiAut#1#2{}%

\def\streszczenie#1{\gdef\@streszczenie{#1}}	
	

\def\keys#1{\gdef\klu@cze{#1}}

\def\dziedzina#1#2{\gdef\@kdznr{#1}\gdef\@kdznazwa{#2}}

\newcount\nr@@klasy 
\nr@@klasy=0


\def\klasa#1#2{\global\advance\nr@@klasy1\relax
\expandafter\gdef\csname klasa@@a\the\nr@@klasy\endcsname{#1}%
\global\advance\nr@@klasy1
\expandafter\gdef\csname klasa@@a\the\nr@@klasy\endcsname{#2}%
}





\long\def\dedykacja#1{\gdef\@podziek{#1}%
                 \let\podziek\ewpodziek }

\let\podziek\relax
\promotor{\WyjasnienieProm }
\def\rok#1{\gdef\@rok{#1}}
\rok{\the\year}
\let\xxdocument\document
\def\document{%
\Jestfalse
\CzyJest{fleqn.clo}%
\ifJest
     \ifJestleqno
        {\DodajNaPoczatek{\mathindent=45pt}}%
\fi\fi
\xxdocument \relax
\let\xxprintindex\printindex
\def\printindex{\newoddpage
\addcontentsline{toc}{section}{Skorowidz}%
\let\newoddpage\newoddpagexxx
\xxprintindex }
\poczatek }


\def\ZmianyOstateczne{}
\def\DodajNaPoczatek{\g@addto@macro\ZmianyOstateczne}


\def\nic{ }
%\def\newcline{\x@protect \newcyline\protect \newcxline }


\def\newlineSpis{\x@protect \\\protect \relax }
\def\newlineTekst{\x@protect \relax \protect \\}

\let\newlineB\newlineTresc
\let\newlineA\newlineSpis

%\def\newcyline{a}
%\def\newcxline{b}


\def\poczatek{\ZmianyOstateczne
\let\ZmianyOstateczne\@empty
\StonaTyt
 \hbox{}\thispagestyle{empty}
 \wyjasnienia
\newpage
\podziek
\global\count0=1
%% nowe
\stronaDruga
\stronaTrzecia

\gdef\kernsekcyjny{}
\gdef\kernsubsekcyjny{}
\gdef\kernsubsubsekcyjny{}
\global\let\savenumberline\numberline
\def\numberline##1{\hbox{##1. }}
%% zwi¹zane z mo¿liwoœci¹ pisania Rozdzia³ 1. itd
\def\numberlineN##1{\hbox{##1. }}
\def\numberlineR##1{\hbox{Rozdzia\l \ ##1. }}

\spistresc

\global\let\numberline\savenumberline
\wstep
}

\def\stronaDruga{
\thispagestyle{empty}
\noindent
Projekt in\.zynierski:\\
\textbf{\@title}\vrule width0pt\\
%%prowadz\k acy: \@promotor %% poprawka 21.05.2014
kieruj\k acy projektem: \@promotor

\vskip2cm

\t@mpnumer=0
%%TODO: zmieniæ na ?
%%%\begin{tabular}{rlp{10cm}}
\begingroup
\leftskip=2cm
\parindent=0pt

 \wypisz@autorowcdx

\endgroup
%%%\end{tabular}

\vfill

\begin{minipage}[t]{8cm}\t@mpnumer=0
\noindent \nazwa@autorowb

\bigskip

\begin{tabular}{rl}
 \wypisz@autorowcdxx
\end{tabular}
\end{minipage}
\hfill
\begin{tabular}[t]{c}
Podpis kieruj\k acego projektem\\[20pt]
\dotfill
\end{tabular}

\break
\thispagestyle{empty}
\hbox{}
\newpage
}

\def\element@aut#1#2#3#4{\noindent\hskip-2cm #1. \textbf{#2} -- (#3\%)\par#4\par\bigskip\par}

\def\stronaTrzecia{
\renewcommand\baselinestretch{1}\selectfont
\thispagestyle{empty}
\subsection*{O\'swiadczenie kieruj\k acego projektem in\.zynierskim}
Potwierdzam, \.ze niniejszy projekt zosta\l\ przygotowany pod
moim kierunkiem i~kwalifikuje si\k e do przedstawienia go w 
post\k epowaniu o nadanie tytu\l u zawodowego:
in\.zynier.
\par\vskip1cm\par Data \hfill Podpis kieruj\k acego projektem

\bigskip

\subsection*{O\'swiadczenie \nazwa@autorowa}

%%%\'Swiadom odpowiedzialno\'sci prawnej o\'swiadczam, \.ze niniejsze opracowanie
%%%zosta\l o napisane  samodzielnie i nie zawiera tre\'sci uzyskanych w spos\'ob niezgodny 
%%%z~obowi\k azuj\k acym prawem.\par
%%%O\'swiadczam r\'ownie\.z, \.ze przedstawione opracowanie 
%%%nie by\l o wcze\'sniej przedmiotem procedur zwi\k azanych
%%%z uzyskaniem tytu\l u zawodowego w  wy\.zszej uczelni.\par
%%%O\'swiadczam ponadto, \.ze niniejsza wersja opracowania 
%%%jest identyczna z za\l \k aczon\k a wersj\k a elektroniczn\k a.

\'Swiadomy/a odpowiedzialno\'sci karnej o\'swiadczam, \.ze 
%przedk\l adana praca magisterska/projekt in\.zynierski/ praca licencjacka na temat:\\
przedk\l adany projekt in\.zynierski na temat:\\
\textbf{\@title}\\
zosta\l\  napisany przez autor\'ow samodzielnie.\\
Jednocze\'snie o\'swiadczam, \.ze ww. projekt:
\begin{itemize}
\item[--] nie narusza praw autorskich w rozumieniu ustawy z~dnia 4~lutego~1994 roku 
     o~prawie autorskim i~prawach pokrewnych (Dz.U. z 2000 r. Nr 80, poz. 904, z p\'o\'zn. zm.) 
     oraz d\'obr osobistych chronionych prawem cywilnym, a tak\.ze nie zawiera danych i informacji, 
     kt\'ore uzyska\l em/am w spos\'ob niedozwolony,
\item[--] nie by\l a wcze\'sniej podstaw\k a \.zadnej innej urz\k edowej 
        procedury zwi\k azanej z nadawaniem dyplom\' ow wy\.zszej uczelni lub tytu\l \'ow zawodowych.
\item[--] nie zawiera fragment\'ow 
        dokument\'ow kopiowanych z innych \'zr\'ode\l\ bez wyra\'znego zaznaczenia i podania \'zr\'od\l a.
\end{itemize}

\vfill

%\par\vskip2cm\par Data \hfill 
\begin{minipage}[t]{14cm}\t@mpnumer=0
\noindent \nazwa@autorowb 

\bigskip
\renewcommand\baselinestretch{1.1}\selectfont
\begin{tabular}{rl}
 \wypisz@autorowcdxxx
\end{tabular}
\end{minipage}

\noindent\hfill Gliwice, dnia \hbox to 4cm{\dotfill}

\renewcommand\baselinestretch{1.3}\selectfont
\newpage
\thispagestyle{empty}
\hbox{}
\newpage
}


\def\next@@wiersz{\\}\def\next@@kom{&}

\newcount\t@mpnumer
\def\wypisz@Klasy{\t@mpnumer=0
\begin{tabular}{ll}
\wypisz@klasycd
\end{tabular}
}



\def\wypisz@klasycd{\global\advance\t@mpnumer1		
	\ifnum\t@mpnumer<\nr@@klasy\relax
			\csname klasa@@a\the\t@mpnumer\endcsname
			\global\advance\t@mpnumer1\relax
			\next@@kom
			\csname klasa@@a\the\t@mpnumer\endcsname
			\next@@wiersz
			\let\next\wypisz@klasycd
	\else \let\next\relax\fi
	\next
	}


%%%%%%%%%%%\def\wypisz@autorow{\t@mpnumer=0
%%%%%%%%%%%\begin{tabular}{l}
%%%%%%%%%%%\wypisz@autorowcd
%%%%%%%%%%%\end{tabular}
%%%%%%%%%%%}

\def\wypisz@autorowcd{\global\advance\t@mpnumer1
	\ifnum\t@mpnumer>\nr@@tora\relax
	\let\next\relax
	\else
			\csname aut@@or\the\t@mpnumer\endcsname
			\next@@wiersz
			\let\next\wypisz@autorowcd
		\fi			
	\next
	}


\def\wypisz@autorowcdx{\global\advance\t@mpnumer1
	\ifnum\t@mpnumer>\nr@@tora\relax
	\let\next\relax
	\else
	\element@aut{\the\t@mpnumer}{\csname aut@@or\the\t@mpnumer\endcsname}{\csname proc@@or\the\t@mpnumer\endcsname}{\csname opaut@@or\the\t@mpnumer\endcsname}
			\let\next\wypisz@autorowcdx
		\fi			
	\next
}

\def\wypisz@autorowcdxx{\global\advance\t@mpnumer1
	\ifnum\t@mpnumer>\nr@@tora\relax
	\let\next\relax
	\else
			\the\t@mpnumer.
			\next@@kom
			\hbox to 4cm{\dotfill}
			\next@@wiersz
			\let\next\wypisz@autorowcdxx
		\fi			
	\next
}
	
		
\def\wypisz@autorowcdxxx{\global\advance\t@mpnumer1
	\ifnum\t@mpnumer>\nr@@tora\relax
	\let\next\relax
	\else
			\the\t@mpnumer.
			\next@@kom
			\hbox to 12cm{\csname aut@@or\the\t@mpnumer\endcsname, \hfill \hbox to 6cm{{\scriptsize nr albumu:}\csname album@@or\the\t@mpnumer\endcsname, \lower4pt\hbox to 0pt{\hskip30pt\tiny (podpis:)\hss} \dotfill}}
			\next@@wiersz
			\let\next\wypisz@autorowcdxxx
		\fi			
	\next
}
	
		
	


\def\ewpodziek{%
  \thispagestyle{empty}%
  \hrule height0pt depth0pt
  \vfill\vfill\vfill
  \begingroup
    \rightskip=2cm
    \leftskip=4cm plus 1fill
    {\large\itshape
      \@podziek
    }
    \vskip2cm
    \vfill
  \endgroup
   \newpage
  \hbox{}\thispagestyle{empty}
  \newpage
}

\def\wstep{%
  \ustawKernyNorm
   \section*{Wst\k ep}%
    \addcontentsline{toc}{section}{Wst\k ep}%
    \markboth{WST\k EP\liniaA}{\liniaB WST\k EP}
     }

\def\wnioski{%
  \ustawKernyNorm
   \section*{Wnioski}%
    \addcontentsline{toc}{section}{Wnioski}%
    \markboth{WNIOSKI\liniaA}{\liniaB WNIOSKI}
     }
     
\def\rysunki{%
  \ustawKernyNorm
   \section*{Rysunki}%
    \addcontentsline{toc}{section}{Wnioski}%
    \markboth{RYSUNKI\liniaA}{\liniaB RYSUNKI}
     }
     
\def\dodatkowo#1{%
  \ustawKernyNorm
   \section*{#1}%
    \addcontentsline{toc}{section}{#1}%
    \markboth{\MakeUppercase{#1}\liniaA}{\liniaB \MakeUppercase{#1}}
     }

     
\let\spistresc\tableofcontents

\def\StonaTyt{\thispagestyle{empty}
    \GornaczescSytronyTyT
    \odstepGoraTytNap
    \wypisRodzajuPracy
    \odstepGoraTyt
    \wypisanieTyt
    \odsteptytAutorProm
    \wypisanieAutorPromotor
    \odstepAutorPromDol
    \dolnaczescStronyTyt
    \wykonajInneOperacje
}
\def\GornaczescSytronyTyT{%
         \begin{center}\fontnag
%% poprawka spowodowana zmian¹ nazwy uczelni
%%         Politechnika \'Sl\k aska w Gliwicach\\
                  Politechnika \'Sl\k aska\\
                  %%
         %%Wydzia\l\ Matematyczno-Fizyczny\\
         Wydzia\l\ Matematyki Stosowanej\\
         Kierunek \kierunek\\
         Studia stacjonarne I stopnia
      \end{center}\par
      }
\def\wypisanieTyt{\begin{center}\fonttyt
\@title
\end{center}
}


\def\wypisRodzajuPracy{\begin{center}{\fontrodzpracy
\TheRodzPracy}\end{center}}

%% poprawka do wprowadzania nowej linii do promotora 24.06.2010
\def\defspec{\def\\{\egroup\hbox\bgroup\fontpromotor}}

\def\wypisanieAutorPromotor{\hbox to \hsize{
   %\vtop{\hbox{\fontpromotoretyk Kieruj\k acy prac\k a:}\hbox\bgroup\fontpromotor\defspec \@promotor\egroup}\hfill
   \vtop{\hbox{\fontpromotoretyk Kieruj\k acy projektem:}\hbox\bgroup\fontpromotor\defspec \@promotor\egroup}\hfill
%%   \vtop{\hbox{\fontautoretyk Autor:}\hbox{\fontautor\@author}}}
\vtop{\hbox{\fontautoretyk \nazwa@autorow:}
      \hbox{\global\t@mpnumer=0%
      \begin{tabular}[t]{l}%      
      \wypisz@autorowcd
      \end{tabular}
      }}}
}

\def\dolnaczescStronyTyt{%
    \begin{center}
   {\fontmiasto \miastoname\ \@rok }
     \end{center}\par\break
}



\def\odstepGoraTyt{\vskip-1pt\vskip0.5in\par\nointerlineskip}
\def\odstepGoraTytNap{\vfill\vfill}
\def\odsteptytAutorProm{\vfill\vfill\vfill\vfill\vfill\vfill}
\def\odstepAutorPromDol{\vskip0.5in\vfill}


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% Wysokosc,
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
\renewcommand\baselinestretch{1.3}
\def\marginesGorny{2.0cm}
\topmargin=-1in
\advance\topmargin \marginesGorny\relax
\@settopoint\topmargin
\textheight=22.0cm
%
% wyrownanie wysokosci dla bez akapitow
%
\@tempdimb \baselinestretch\baselineskip
\@tempdima\textheight
\divide\@tempdima\@tempdimb
\@tempcnta=\@tempdima
\setlength\textheight{\@tempcnta\@tempdimb}
\addtolength\textheight{\topskip}
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%  szeroksci
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% margines szcz\k atkowy
%
\marginparwidth=3mm
\@settopoint\marginparwidth

%
% Dane o marginesach i szyciu, reszata wyliczana przy zalozeniu 21cm szer.
%
\def\szerMarginesu{2cm}
\def\szerNaSzycie{2cm}

%
% wylicz szerokosc tekstu
%
\textwidth=21cm
\advance\textwidth by -\szerMarginesu\relax
\advance\textwidth by -\szerMarginesu\relax
\advance\textwidth by -\szerNaSzycie\relax

%
%  wpierw margines nieparzysty -1in == usunac hoffset
%                              potem na normalny margines bez szycia
%
\oddsidemargin=-1in
\advance\oddsidemargin\szerMarginesu


%
%  tu rowniez parzysty margines taki
%
\evensidemargin\oddsidemargin

%
%  nieparzysty zawsze ma miejsce na szycie
%
\advance\oddsidemargin by \szerNaSzycie

%
%  parzysty na szycie tylko w druku jesnostronnym:
%
\if@twoside \else
 \advance\evensidemargin by\szerNaSzycie
\fi

%
% wszelkie szerokosci wyrownac do pt.
%
\@settopoint\oddsidemargin
\@settopoint\evensidemargin
\@settopoint\textwidth
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%



%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%  naglowki
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

\def\liniaA{\vrule width-\textwidth depth3pt height-2.6pt
          \vrule width\textwidth depth3pt height-2.6pt\relax}
\def\liniaB{\vrule width\textwidth depth3pt height-2.6pt
          \vrule width-\textwidth depth3pt height-2.6pt\relax}


\pagestyle{myheadings}
\markboth{\MakeUppercase{Spis Tre\'sci}\liniaA}{\liniaB \MakeUppercase{Spis Tre\'sci}}






%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%  r¢wnania
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
\let\PrzedSection\relax
\let\PrzedSubSection\relax

\let\PrzedSectionx\relax
\let\PrzedSubSectionx\relax

\def\zerujLicznikRownan{\setcounter{equation}{0}}
\def\zerujLicznikRysunkow{\setcounter{figure}{0}}
\def\sectionWzory{\renewcommand \theequation {\thesection.\@arabic\c@equation}%
      \let\PrzedSection\zerujLicznikRownan
      \let\PrzedSubSection\relax          }
\def\subsectionWzory{\renewcommand \theequation {\thesubsection.\@arabic\c@equation}%
      \let\PrzedSection\zerujLicznikRownan
      \let\PrzedSubSection\zerujLicznikRownan
      }


\def\sectionRysunki{\renewcommand \thefigure {\thesection.\@arabic\c@figure}%
      \let\PrzedSectionx\zerujLicznikRysunkow
      \let\PrzedSubSectionx\relax          }


\def\subsectionRysunki{\renewcommand  \thefigure {\thesubsection.\@arabic\c@figure}%
      \let\PrzedSectionx\zerujLicznikRysunkow
      \let\PrzedSubSectionx\zerujLicznikRysunkow          }



%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
%  sekcja do nagˆ¢wka
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
\def\ustawKernyNorm{
\def\kernsekcyjny{\kern-1em. }
\def\kernsubsekcyjny{\kern-1em. }
\def\kernsubsubsekcyjny{\kern-1em. }
}

\let\sectionXXX\section
\def\sectionXY[#1]#2{\def\NagText{\thesection. #1}%
\sectionXXX{\protect\kernsekcyjny #2}%
\markboth{\MakeUppercase\NagText\liniaA}{\liniaB\MakeUppercase\NagText}}
\def\section{\@ifnextchar*{\sectionXXX}{\@dblarg\sectionXY}}

% \@dblarg\CS when \CS takes arguments [ARG1]{ARG2},
%                        where default is ARG1 = ARG2.

\DodajNaPoczatek{\def\contentsname{Spis tre\'sci}}
\DodajNaPoczatek{\def\indexname{Skorowidz}}
%\DodajNaPoczatek{\mathindent=45pt}
\def\indexnameX{Skorowidz}
\def\indexnameY{\vbox to 12pt{\vss Skorowidz \vskip15pt\hrule height0pt}\vskip-8.3pt\relax}

\let\subsectionXXX\subsection
\def\subsectionYYY#1{\subsectionXXX{\protect\kernsubsekcyjny #1}}
\def\subsection{\@ifnextchar*{\subsectionXXX}{\subsectionYYY}}

\let\subsubsectionXXX\subsubsection
\def\subsubsectionYYY#1{\subsubsectionXXX{\protect\kernsubsubsekcyjny #1}}
\def\subsubsection{\@ifnextchar*{\subsubsectionXXX}{\subsubsectionYYY}}


%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%% kropki w twierdzeniach
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
\def\@xthm#1#2{\@begintheorem{#2}{\csname the#1\endcsname.}\ignorespaces}
\def\@ythm#1#2[#3]{\@opargbegintheorem{#2}{\csname the#1\endcsname.}{#3}\ignorespaces}

\def\NumeryNaPoczatku{
\def\@xthm##1##2{\@begintheorem{\csname the##1\endcsname.}{##2}\ignorespaces}
\def\@ythm##1##2[##3]{\@opargbegintheorem{\csname the##1\endcsname.}{##2}{##3}\ignorespaces}
}
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%
%% DODATKI
%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

\newcounter{numeracjadod} \setcounter{numeracjadod}{0}
\def\dodatekNr{\ewspac\spos{numeracjadod}}
\def\usun#1{}

\def\rzymskaNumeracjaDodatkow{\let\spos\Roman \def\ewspac{ }}
\def\literowaNumeracjaDodatkow{\let\spos\Alph \def\ewspac{ }}
\def\bezNumeracjiDodatkow{\let\spos\usun \def\ewspac{}}

\bezNumeracjiDodatkow

\def\dodatek{\addtocounter{numeracjadod}{1}%
\@ifnextchar*{\dodatekX}{\@dblarg\dodatekY}}
\def\dodatekX*{\@dblarg\dodatekXY}


\def\dodatekY[#1]#2{\def\xtmp{#1}%
\section*{Dodatek\dodatekNr}\vskip-30pt
\subsection*{#2}\vskip30pt
\ifx\xtmp\@empty
\def\NagText{Dodatek\dodatekNr}%
\else
\def\NagText{Dodatek\dodatekNr: #1}%
\fi
\markboth{\MakeUppercase\NagText\liniaA}{\liniaB\MakeUppercase\NagText}%
\def\xtmp{#2}%
\ifx\xtmp\@empty
\addcontentsline{toc}{section}{Dodatek\dodatekNr}%
\else
\addcontentsline{toc}{section}{Dodatek\dodatekNr: #2}%
\fi
}

\def\dodatekXY[#1]#2{\def\xtmp{#1}%
\section*{Dodatek\dodatekNr}\vskip-30pt
\subsection*{#2}\vskip30pt
\ifx\xtmp\@empty
\def\NagText{Dodatek\dodatekNr}%
%\addcontentsline{toc}{section}{Dodatek\dodatekNr}%
\else
\def\NagText{Dodatek \dodatekNr: #1}%
%\addcontentsline{toc}{section}{Dodatek\dodatekNr: #2}%
\fi
\markboth{\MakeUppercase\NagText\liniaA}{\liniaB\MakeUppercase\NagText}%
}



\author{}
\title{}

%%\def\pracaMagisterska{\def\TheRodzPracy{Praca dyplomowa magisterska}}
%%\def\pracaInzynierska{\def\TheRodzPracy{Praca dyplomowa in\.zynierska}}
%%\def\projektInzynierski{\def\TheRodzPracy{Projekt in\.zynierski}}
\def\TheRodzPracy{Projekt in\.zynierski}


\rodzajKodow



%\def\usunkropke#1{}

\def\@seccntformatSPEC#1{
\begingroup
\lower-2cm\hbox to 0pt{Rozdzia\l\
%\let\protect\usunkropke
\csname the#1\endcsname\hss }\endgroup}
\def\znikajKropko{}%
\def\rozdzialy{%
   \let\seczwykform\@seccntformat
   \let\sectionXYZ\section
   \def\section{\let\kernsekcyjny\znikajKropko \let\@seccntformat\@seccntformatSPEC\sectionXYZ }
   \let\subsectionXYZ\subsection
   \def\subsection{\let\@seccntformat\seczwykform\subsectionXYZ }
}




\def\rozdzialyx{\rozdzialy
\renewcommand*\l@section[2]{%
  \ifnum \c@tocdepth >\z@
    \addpenalty\@secpenalty
    \addvspace{1.0em \@plus\p@}%
    \setlength\@tempdima{3em}%
    \let\numberline\numberlineR
    \begingroup
      \parindent \z@ \rightskip \@pnumwidth
      \parfillskip -\@pnumwidth
      \leavevmode \bfseries
      \advance\leftskip\@tempdima
      \hskip -\leftskip
      ##1\nobreak\hfil \nobreak\hb@xt@\@pnumwidth{\hss ##2}\par
    \endgroup
  \fi}
\renewcommand*\l@subsection{\let\numberline\numberlineN \@dottedtocline{2}{1.5em}{2.8em}}			
}


\def\rzymskieRozdzialy{
\renewcommand{\thesection}{\Roman{section}%
 }
\renewcommand{\thesubsection}{\Roman{section}.\arabic{subsection}%
}
\renewcommand{\thesubsubsection}{\Roman{section}.%
       \arabic{subsection}.\arabic{subsubsection}%
}
\renewcommand*\l@section[2]{%
  \ifnum \c@tocdepth >\z@
    \addpenalty\@secpenalty
    \addvspace{1.0em \@plus\p@}%
    \setlength\@tempdima{3em}%
    \begingroup
      \parindent \z@ \rightskip \@pnumwidth
      \parfillskip -\@pnumwidth
      \leavevmode \bfseries
      \advance\leftskip\@tempdima
      \hskip -\leftskip
      ##1\nobreak\hfil \nobreak\hb@xt@\@pnumwidth{\hss ##2}\par
    \endgroup
  \fi}
\renewcommand*\l@subsection{\@dottedtocline{2}{1.5em}{3.8em}}
\renewcommand*\l@subsubsection{\@dottedtocline{3}{3.8em}{5em}}
}

\def\wykonajInneOperacje{}
\def\drukJednostronny{
\advance\evensidemargin by\szerNaSzycie
\def\wykonajInneOperacje{%
    \def\@evenhead{{\slshape\rightmark}\hfil\thepage}%
    \def\@oddhead{{\slshape\rightmark}\hfil\thepage}%
}}


% dla kompatybilnosci:
\let\xle\le \let\xge\ge
%
%\let\rodzajKodow\relax
%\def\rodzajKodow{\usepackage[cp852]{inputenc}}  = opcja dos
%\def\rodzajKodow{\usepackage[cp1250]{inputenc}} = opcja win
%

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%         WYJAŒNIENIA                                       %%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
\def\wyjasnienia{

\noindent
w preambule (czyli przed \texttt{\char`\\begin\char`\{document\char`\}}) musz\k a by\'c 
u\.zyte nast\k epuj\k ace komendy (przyk\l adowo)

\smallskip

\texttt{\char`\\title\char`\{Tytu{\l} pracy \char`\}}

\texttt{\char`\\promotor\char`\{dr in\.z Marek Xsi\'nski\char`\}} 

\medskip
 
\noindent
Dla ka\.zdego autora (liczba 12 jest procentem wk\l adu pracy)

\texttt{\char`\\autor\char`\{Tomasz Xsi\'nski\char`\}\char`\{12\char`\}
\char`\{Opis wk\l adu pracy\char`\}
}

\bigskip

Aby zmieni\'c spos\'ob robienia margines\'ow (nie zmienia numeracji stron) u\.zywamy komendy:

\texttt{\char`\\drukJednostronny} 


\bigskip

\noindent
Spis tre\'sci pojawia si\k e automatycznie. Podobnie Wst\k ep przed pierwszym rozdzia\l em.

\noindent
Kolejne rozdzia\l y wprowadzamy komend\k a:

\smallskip

\texttt{\char`\\section\char`\{Tytu\l\  rozdzia\l u \char`\}}

\smallskip

\noindent
lub gdy tytu\l{} nie mie\'sci si\k e w nag\l\'owku:

\smallskip

\texttt{\char`\\section[skr\'ocony tytu\l{} do nag.\ldots]\char`\{Tytu\l\  rozdzia\l u \char`\}}

\medskip

\noindent
Mo\.zna te\.z zastosowa\'c komendy:

\texttt{\char`\\newlineSpis} --- dzia\l a tylko gdy tytu\l rozdzia\l u jest w spisie tre\'sci

\texttt{\char`\\newlineTekst} ---  dzia\l a tylko gdy tytu\l rozdzia\l u jest w tek\'scie

\bigskip

Mo\'zemy te\.z zrobi\'c dedykacj\k e:

\texttt{\char`\\dedykacja\char`\{Dedykacja\char`\\\char`\\podzielona\char`\\\char`\\na linie\char`\}}


\bigskip

\noindent
Inne ciekawe komendy (ich nazwa powinna wyja\'sni\'c zastosowanie)

\begin{tabular}{|clc|cl}
&\texttt{\char`\\dodatek\char`\{TYT\char`\}} && 
&\texttt{\char`\\dodatek[skr]\char`\{TYT\char`\}}\\
&\texttt{\char`\\dodatek*\char`\{TYT\char`\}}&&
&\texttt{\char`\\wnioski}\\
&\texttt{\char`\\rysunki}&&
&\texttt{\char`\\dodatkowo\char`\{DOWOLNANAZWA\char`\}}\\
&\texttt{\char`\\rzymskaNumeracjaDodatkow}&&
&\texttt{\char`\\literowaNumeracjaDodatkow}\\
&\texttt{\char`\\sectionRysunki}&&
&\texttt{\char`\\subsectionRysunki}\\
&\texttt{\char`\\sectionWzory}&&
&\texttt{\char`\\subsectionWzory}\\
&\texttt{\char`\\rozdzialy}&& 
&\texttt{\char`\\rzymskieRozdzialy}\\
\end{tabular}

\bigskip\bigskip

U\.zyj komendy \texttt{\char`\\bezWyjasnien} aby nie drukowa\'c tej strony

Pytania i uwagi prosz\k e kierowaæ na adres: \texttt{marek.zabka@polsl.pl}

}



\def\bezWyjasnien{\let\wyjasnienia\relax}



%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%         K O N I E C                                       %%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%





\endinput
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%
%% \mathindent=45pt gdy  wzory do lewej i numery do lewej
%%    Poprawic zale¾no˜† od sposobu numerowania wzor¢w
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%


\wnioski
     
\rysunki
     
\dodatkowo{DOWOLNANAZWA}



\rzymskaNumeracjaDodatkow 
\literowaNumeracjaDodatkow
\sectionWzory
\subsectionWzory



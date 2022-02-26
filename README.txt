WordleSolver
by Michael Kazmierski
.NET 5.0

Instructions:
Open the SLN file in Visual Studio and run the program.  When prompted, enter the path, including the file name and extension, in the command prompt.  Press enter.  The program will run and save the results onto your desktop as a txt file.  If the output file does not immediately appear, try right-clicking on the desktop and hit refresh (I occasionally have this problem when saving files to my desktop on my personal PC, I'm not sure if this is a widespread problem or not).

About the dictionary:
Simple calculations/analysis were made to determine the best way to select words for each of the six allowed guesses.  The first two guesses are hard-coded based on the results of the data.  For each letter in the alphabet, the number of words containing that letter in the provided dictionary was added.  Then, each word in the dictionary was given a numeric score calculated by adding up the number found for each letter in that word.  Additionally, each word was identified if the word contains repeated letters or not.  The first seed word, AROSE, was chosen by selecting the word from the dictionary with the highest score that also had no repeated letters.  The second seed word was then found by selecting the word with the highest score, also no repeated letters, and also contained no letters that were in the first seed word.  This second seed word is UNLIT.  The dictionary scoring was performed using R, specifically an R markdown file, and that file has been copied into the Data folder of the SolveWordle project.

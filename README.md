<h1 align="center">Sudoku</h1>
Written by Volodymyr Shabala


![Game Screenshot!](https://github.com/Volodymyr-Shabala/Sudoku/blob/feature/readme/ReadMeFiles/GameScreenshot.png)

This is a copy of a game [Sudoku.com](https://play.google.com/store/apps/details?id=com.easybrain.sudoku.android&hl=en_US) for mobile devices as it was in 2023.
It is not a full copy as some features will be missing. This project is for the purpose displaying my skills and thinking process while coding.

All the basic features of a Sudoku game can be found in this project:
- Sudoku generator
- Basic sudoku gameplay
- Undo system
- Easy extensibility

Features to consider:
- Save system
  - Save seed
  - Save placed numbers and pencil

- Load system
  - Use saved seed to generate a board
  - Create a new _BoardCreator_ which populates saved tile states for the rest of the tiles

Limitations:
- Random seed can't be used to generate board as there is a chance for an unsolvable board to be generated. Seeds need to be generated beforehand.

# Tetris
## Description
Simple Tetris game made in unity. No extra library included.

This project was made as an homework for the Mobile course of the Master in computer game development.

## Controls
Arrow keys L/R to move Left or Right

Arrow key Up to rotate the piece

Arrow key Down to move the piece down


# Issues
1. Rotation often cause the piece to break, it goes inside other block or out of the screen **Fixed**
2. Outer wall don't work with the new right and left collision system - **Fixed**
3. Rotation doesn't reset the collisions flag - **Fixed**
4. L piece can't move left when collision is right, something is wrong with the collision - **Fixed**
5. If you spam left or right when there is a piece down, it doesn't spawn the next block - **Fixed**
6. Rotations reset flag that blocks left and right movement, also collision of the bottom gets reset but not checked again - **Fixed**
7. Rotation of block goes inside the wall if it is near it (checked with the L block) - **Fixed**
8. Function to delete row doesn't work correctly - **Fixed**
9. Rotating and going down would sometimes glitch the block inside another block. - **Fixed**

# Fixes
1. Create a function that checks after being rotated if there are any collision, then try to move up, right, or left and check again, up to 2 times. When there is no collision accept the new location and move the block
2. Replace the outer wall with something similar to the floor, a wall made of block (now they trigger the collisions correctly)
3. Just add the function to reset the flag (ResetCollision or something like that) at the start of the rotate function
4. Check the collisions - Added a ? when checking the position of the collision
5. The block was spawning one block over the top of the screen, if you held right or left it would go out of the screen into the abyss
6. Save the values of the bool flag and reset it if the block doesn't move
7. Added a delay between inputs, so the inputs do not overlap with one another
8. Every row moves down, must move down only the row with a greater y coordinate than the one just deleted
9. Now after the move down I check for any collision and fix the position if the bug occurred.

# Things to add
1. Reduce the width of the screen - **Added**
2. Add the "next piece" part where it shows you the next piece that will come out - **Added**
3. When a line is filled, delete all blocks and move everything down 1 - **Added**
4. If the block spawned is already in collision, end the game. - **Added**

# May be problems
1. CollisionBox create with the function Physics2D.OverlapBox may not detect other block if the first collision is itself - **Fixed**

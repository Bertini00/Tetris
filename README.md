# Tetris

# Issues
1. Rotation often cause the piece to break, it goes inside other block or out of the screen **Fixed**
2. Outer wall don't work with the new right and left collision system - **Fixed**
3. Rotation doesn't reset the collisions flag - **Fixed**
4. L piece can't move left when collision is right, something is wrong with the collision - **Fixed**
5. If you spam left or right when there is a piece down, it doesn't spawn the next block - **Fixed**
6. Rotations reset flag that blocks left and right movement, also collision of the bottom gets reset but not checked again - **Fixed**

# Fixes
1. Create a function that checks after being rotated if there are any collision, then try to move up, right, or left and check again, up to 3 times. When there is no collision accept the new location and move the block
2. Replace the outer wall with something similar to the floor
3. Just add the function to reset the flag (ResetCollision or something like that) at the start of the rotate function
4. Check the collisions - Added a ? when checking the position of the collision
5. The block was spawning one block over the top of the screen, if you held right or left it would go out of the screen into the abyss
6. Save the values of the bool flag and reset it if the block doesn't move

# Things to add
1. Reduce the width of the screen
2. Add the "next piece" part where it shows you the next piece that will come out
3. When a line is filled, delete all blocks and move everything down 1

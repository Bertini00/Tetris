# My project

# Issues
1. Rotation often cause the piece to break, it goes inside other block or out of the screen
2. Outer wall don't work with the new right and left collision system
3. Rotation doesn't reset the collisions flag

# Probable Fixes
1. Create a function that checks after being rotated if there are any collision, then try to move up, right, or left and check again, up to 3 times. When there is no collision accept the new location and move the block
2. Replace the outer wall with something similar to the floor
3. Just add the function to reset the flag (ResetCollision or something like that) at the start of the rotate function

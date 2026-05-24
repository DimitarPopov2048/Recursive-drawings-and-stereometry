# Recursive-drawings-and-stereometry
This is a imple file with several methods for drawing. I tried to implement everything by myself, only with geometry and use a minimum amount of built-in functions.

# Methods

We have several different methods that I use: DrawTree, DrawPolygonK, PCube, Pyr. The P stands for perspective.

DrawTree is the most basic thing anyone can start with. We have a tree, keep drawing and decrement the length every time until a threshold. There is an option to rotate it at an angle clockwise.
<img width="747" height="321" alt="Trees Overall" src="https://github.com/user-attachments/assets/c292297d-fd96-49a0-90da-50a6e718a29b" />
#
For drawK, we input a number n. Then, we draw an n-sided polygon and the inscribed circle, then that circle becomes the outscribed circle for the next n-1-sided polygon and we continue until a triangle. We also have the option to rotate the entire thing at an angle counterclockwise.
<img width="665" height="597" alt="8" src="https://github.com/user-attachments/assets/7cf364a1-5c46-4c1d-bc53-d461f69d4699" />
<img width="1273" height="591" alt="DrawK2" src="https://github.com/user-attachments/assets/67ecf275-c850-4a7f-afa0-c3bf06bb5348" />
#
For the cube we have an input angle between the sides of the base and we draw it in this way with a custom method for dashed-line. Then there is a picture where 4 cubes intersect and it looks really cool, like a napkin. For the pyramid, we input a number n for the sides of the base, a height and an angle that rotates the base counterclockwise.
<img width="1267" height="521" alt="stereometry" src="https://github.com/user-attachments/assets/a927c1f0-4439-467b-9efe-d7fb3925b315" />
#
There is also a method for prism, but it is the same as the others, you cann try it if you are interested.


This is a list of various design decisions and things to note about my solution:

1.) The Log class was specifically created in case you have a test harness you 
want to run my solution against. The public properties "WriteToConsoleEnabled"
and "WaitForKeyPressEnabled" can both be set to false in order to disable all
the input and output.

The Log class could easily be extended to write to a log file as well, but it 
wasn't necessary in order for me to test and debug.


2.) When outputing the grid to the console I made it such that it would mimic
the example where (0,0) is in the lower left corner.


3.) Given that a.) cell locations in the sample are specified as (X, Y), and 
b.) C# two dimensional array normally map to [row, column] or [Y, X], this would 
mean that a cell location (3, 2) would normally be written as "grid[2, 3]". However, 
I found this made debugging more difficult since I constantly had to reverse the X and 
Y values in my head when comparing the results to known values. So, in my code the array 
actually uses [column, row] or [x, y] which matches exactly the order of the cell values. 
However, the side effect is that when initializing the two dimensional array you must 
initialize it in column order instead of row order. For example, if you have a 2x2 grid 
that looks like:
   15 20
    5 10
and the "5" value represents location (0,0), you would initialize the array as 
"int[,] grid = new int[2,2]{{5, 15}, {10,20}};". 

It would be trivial to change the code to use [y, x] throughout and change the 
initializers to initialize in row order instead of column order. However, I really 
liked how this made debugging easier.


4.) For the center of mass calculation I assumed you wanted them in floating point 
values since that's the only thing that makes sense to show when the center of mass
doesn't fall on an even cell boundry. However, we can round the values to integers
if desired.

5.) I know I wrote several classes that are somewhat trivial and technically could be
skipped. I'm specifically talking about the classes that just contain a list of other
classes. I only did this so: a.) I could encapsalate the logging methods within
the list classes, and b.) similarly, the calculations for center of mass
are encapsulated within the class itself instead of a separate method that
takes a region as a parameter. Also this allows us to change the storage methods to use
array or graphs later if desired. However, it could be argued this makes the code
larger and more complex than it needs to be.


5. I have some very rudimentary test cases in my program with some very simple
testing procedures. In the real world I would use NUnit to write a suite of test cases
on all of the classes and their methods. 


6. Next steps: If I was actually implementing this as part of a my job I would, as noted 
previously, use NUnit to implement both unit tests and more test cases. I don't feel my
list of tests is complete, but they cover the basics. Then I would probably write a
graphical interface on top of this to display the grid with "heat map" like colors to 
indicate the regions that meet the threshold and then a dot to indicate the center of mass 
of each region. Finally, this would allow us to change the threshold dynamically so that 
we can see what effect that has on various grids.

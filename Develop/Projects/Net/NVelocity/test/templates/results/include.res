

#*

@test include.vm

This template is used for Velocity regression testing.
If you alter this template make sure you change the
corresponding comparison file so that the regression 
test doesn't fail incorrectly.

*#

#include("include.vm" "include.vm")

#set($foo = "subdir/test.txt")

#include($foo)

#*

@test include.vm

This template is used for Velocity regression testing.
If you alter this template make sure you change the
corresponding comparison file so that the regression 
test doesn't fail incorrectly.

*#

#include("include.vm" "include.vm")

#set($foo = "subdir/test.txt")

#include($foo)



This is included text!



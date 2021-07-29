#! /usr/bin/bash

# Multi-functional program
# First half checks if a provided number is odd or even
# Second half interperets a students letter grade from a mark score
# Emery Hart - 7/29/2021


# This is the begining of the number checking code

read -p "Enter any number: " number

if [ $(( $number % 2 )) == 0 ]
then
    echo "That Number was an Even Number!"
else
    echo "That Number was an Odd Number!"
fi

# This is the end of the number checking code

# This is he beginning of the grade interpereter

read -p "Enter the marks you recived: " marks

if  [ $marks -gt 100 ] || [ $marks -lt 0 ]
then
    echo "Invalid Marks!"
elif [ "$marks" -gt 39 ] && [ "$marks" -lt 51 ]
then
    echo "You have earned a D!"
elif [ "$marks" -gt 50 ] && [ "$marks" -lt 61 ]
then
    echo "You have earned a C!"
elif [ "$marks" -gt 60 ] && [ "$marks" -lt 71 ]
then
    echo "You have earned a B!"
elif [ "$marks" -gt 70 ]
then
    echo "You have earned an A!"
else
    echo "You have earned an F!"
fi

# End of the program 
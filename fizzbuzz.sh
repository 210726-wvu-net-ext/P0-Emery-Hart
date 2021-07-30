#! /usr/bin/bash

#Start of "Fizzbuzz" loop

n=1
while [ $n -le 20 ]
do
    if [ $(( $n%3 )) -eq 0 ] && [ $(( $n%5 )) -eq 0 ]
        then
            echo "Fizzbuzz - " $n
        elif [ $(( $n%5 )) -eq 0 ]
        then
            echo "Buzz - " $n
        elif [ $(( $n%3 )) -eq 0 ]
        then
            echo "Fizz - " $n
        fi
    #n=$(( $n+1 ))
    (( n++ ))
done
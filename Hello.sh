#! /usr/bin/bash

#Prompts user for three favorite movies and then prints them 

echo "Enter your three favorite movies (separated by a single space, uses a - for in-title spaces): "
read -a movies 
echo "The movies are: ${movies[0]}, ${movies[1]}, ${movies[2]}"
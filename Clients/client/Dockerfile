# # Build stage
# FROM node:alpine as node

# WORKDIR /src
# COPY . .

# RUN npm install && npm run build 

# # Run Stage
# FROM nginx:alpine
# COPY nginx.conf /etc/nginx/nginx.conf
# COPY --from=build /app/dist/ /usr/share/nginx/html






# Use an official Node.js runtime as a parent image
FROM node:14-alpine as build

# Set the working directory to /app
WORKDIR /usr/local/app/



# Copy the package.json and package-lock.json files to the container
COPY ./Clients/client /usr/local/app/


# Install dependencies
RUN npm install

# Build the application
RUN npm run build

# Use an official Nginx runtime as a parent image
FROM nginx:stable-alpine

# Copy the build output from the build image to the Nginx image
COPY --from=build /usr/local/app/dist/client /usr/share/nginx/html

# Usuń domyślny plik nginx.conf
RUN rm /etc/nginx/nginx.conf

COPY ./Clients/client/ssl/server.crt /etc/ssl/certs/
COPY ./Clients/client/ssl/server.key /etc/ssl/private/
COPY ./Clients/client/nginx.conf /etc/nginx/nginx.conf

# Zmień uprawnienia pliku
RUN chown nginx:nginx /etc/nginx/nginx.conf

# Expose port 80
EXPOSE 80
EXPOSE 443

#FROM node:14-alpine AS build
#WORKDIR /usr/src/app
#COPY ./Clients/client/package.json ./
#RUN npm install
#COPY . .
#RUN npm run build

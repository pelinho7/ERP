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
COPY ./ /usr/local/app/

# Install dependencies
RUN npm install

# Build the application
RUN npm run build

# Use an official Nginx runtime as a parent image
FROM nginx:stable-alpine

# Copy the build output from the build image to the Nginx image
COPY --from=build /usr/local/app/dist/client /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start Nginx
# CMD ["nginx", "-g", "daemon off;"]

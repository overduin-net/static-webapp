# Gebruik een basisimage met .NET 8 en Node.js 20
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

# Installeer Node.js 20
RUN curl -fsSL https://deb.nodesource.com/setup_20.x | bash - \
&& apt-get install -y nodejs

# Create the node user
RUN useradd -ms /bin/bash node

# Install global npm packages
RUN npm install -g typescript eslint azurite

# Set the working directory
WORKDIR /workspace

# Copy the contents of the current directory to the container
COPY . .

# Change ownership of the workspace directory to the node user
RUN chown -R node:node /workspace

#TODO azurite won't start in background
#RUN azurite &
#ENV NG_FORCE_TTY=false

# Switch to the node user
USER node
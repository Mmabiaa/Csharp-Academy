FROM node:18-alpine AS build
WORKDIR /app

# Accept build arguments for Vite environment variables
ARG VITE_API_URL
ARG VITE_GOOGLE_CLIENT_ID
ENV VITE_API_URL=$VITE_API_URL
ENV VITE_GOOGLE_CLIENT_ID=$VITE_GOOGLE_CLIENT_ID

COPY . .
# Skip npm install and use local node_modules copied in the step above
# Note: This assumes node_modules exists in the context directory
RUN npm run build

FROM nginx:stable-alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
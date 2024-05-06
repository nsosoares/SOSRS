FROM node:lts-alpine as angular
WORKDIR /app
COPY . /app
RUN npm install
COPY . .
RUN npm run build

FROM nginx:alpine
VOLUME /var/cache/nginx
COPY --from=angular app/dist/imobmais.backoffice.imobiliaria.front/browser /usr/share/nginx/html
COPY ./config/nginx.conf /etc/nginx/conf.d/default.conf

#docker build -t sosrs.api:latest .
#docker run -p 8089:80 sosrs.api:latest

FROM node:16
WORKDIR /app

COPY Studenda.Web.Client/package*.json ./
RUN npm install

COPY Studenda.Web.Client/. .
# RUN npm run build

EXPOSE 8080

CMD [ "npm", "run", "dev" ]

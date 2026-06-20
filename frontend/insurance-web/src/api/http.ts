import axios from 'axios';

export const http = axios.create({
  baseURL: import.meta.env.VITE_API_URL ?? 'https://localhost:7001/api',
  headers: {
    'Content-Type': 'application/json'
  }
});

import { http } from './http';
import type {
  ApiResponse,
  Cliente,
  Cobertura,
  EmitirPolizaRequest,
  PolizaResumen
} from '../types/api';

export async function getClientes(): Promise<Cliente[]> {
  const response = await http.get<ApiResponse<Cliente[]>>('/clientes');
  return response.data.data;
}

export async function getCoberturas(): Promise<Cobertura[]> {
  const response = await http.get<ApiResponse<Cobertura[]>>('/coberturas');
  return response.data.data;
}

export async function getPolizas(): Promise<PolizaResumen[]> {
  const response = await http.get<ApiResponse<PolizaResumen[]>>('/polizas');
  return response.data.data;
}

export async function emitirPoliza(payload: EmitirPolizaRequest) {
  const response = await http.post('/polizas/emitir', payload);
  return response.data;
}

export type ApiResponse<T> = {
  success: boolean;
  message: string;
  data: T;
};

export type Cliente = {
  id: number;
  nombre: string;
  identificacion: string;
  correo: string;
  telefono?: string | null;
};

export type Cobertura = {
  id: number;
  nombre: string;
  descripcion?: string | null;
  montoCobertura: number;
};

export type VehiculoRequest = {
  placa: string;
  marca: string;
  modelo: string;
  anio: number;
  valorComercial: number;
};

export type EmitirPolizaRequest = {
  clienteId: number;
  vehiculo: VehiculoRequest;
  coberturasIds: number[];
};

export type PolizaResumen = {
  id: number;
  numeroPoliza: string;
  cliente: string;
  vehiculo: string;
  fechaEmision: string;
  sumaAsegurada: number;
  primaTotal: number;
  estado: string;
};

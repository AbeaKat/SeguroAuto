import { useState } from 'react';
import { useQuery } from '@tanstack/react-query';
import { getPolizas } from '../api/insuranceApi';
import { formatCurrency } from '../lib/utils';
import type { PolizaResumen } from '../types/api';
import { X } from 'lucide-react';

export function PoliciesPage() {
  const [selectedPoliza, setSelectedPoliza] = useState<PolizaResumen | null>(null);

  const { data, isLoading, isError } = useQuery({
    queryKey: ['polizas'],
    queryFn: getPolizas
  });

  const handleCloseModal = () => {
    setSelectedPoliza(null);
  };

  if (isLoading) {
    return <p className="text-slate-600">Cargando pólizas...</p>;
  }

  if (isError) {
    return <p className="text-red-600">No se pudo cargar el historial de pólizas.</p>;
  }

  return (
    <>
      <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
        <h2 className="text-xl font-semibold text-slate-900">Historial de emisiones</h2>
        <p className="mt-1 text-sm text-slate-500">
          Listado de pólizas emitidas.
        </p>

        <div className="mt-6 overflow-x-auto">
          <table className="w-full border-collapse text-left text-sm">
            <thead>
              <tr className="border-b bg-slate-50 text-slate-600">
                <th className="p-3">Numero</th>
                <th className="p-3">Cliente</th>
                <th className="p-3">Vehiculo</th>
                <th className="p-3">Prima</th>
                <th className="p-3">Estado</th>
              </tr>
            </thead>
            <tbody>
              {data?.map((poliza) => (
                <tr key={poliza.id} className="border-b last:border-none hover:bg-slate-50">
                  <td className="p-3 font-medium">
                    <button
                      onClick={() => setSelectedPoliza(poliza)}
                      className="text-[#43bb6c] hover:text-[#2d8a52] hover:underline cursor-pointer transition-colors"
                    >
                      {poliza.numeroPoliza}
                    </button>
                  </td>
                  <td className="p-3">{poliza.cliente}</td>
                  <td className="p-3">{poliza.vehiculo}</td>
                  <td className="p-3">{formatCurrency(poliza.primaTotal)}</td>
                  <td className="p-3">{poliza.estado}</td>
                </tr>
              ))}

              {data?.length === 0 && (
                <tr>
                  <td className="p-3 text-slate-500" colSpan={5}>
                    Todavia no hay pólizas emitidas.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </section>

      {selectedPoliza && (
        <>
          <div
            className="fixed inset-0 z-40 bg-black/50"
            onClick={handleCloseModal}
          />
          <div className="fixed inset-0 z-50 flex items-center justify-center p-4">
            <div className="w-full max-w-2xl rounded-2xl bg-white p-8 shadow-xl">
              <div className="flex items-center justify-between mb-6">
                <h2 className="text-2xl font-bold text-slate-900">Detalle de Poliza</h2>
                <button
                  onClick={handleCloseModal}
                  className="rounded-lg p-2 hover:bg-slate-100 transition-colors"
                >
                  <X size={24} className="text-slate-600" />
                </button>
              </div>

              <div className="space-y-6">
                <div className="border-b border-slate-200 pb-6">
                  <div className="grid grid-cols-2 gap-4">
                    <div>
                      <p className="text-sm text-slate-600">Numero de Póliza</p>
                      <p className="text-lg font-semibold text-slate-900">{selectedPoliza.numeroPoliza}</p>
                    </div>
                    <div>
                      <p className="text-sm text-slate-600">Estado</p>
                      <p className="text-lg font-semibold text-slate-900">{selectedPoliza.estado}</p>
                    </div>
                    <div>
                      <p className="text-sm text-slate-600">Fecha de Emisión</p>
                      <p className="text-lg font-semibold text-slate-900">
                        {new Date(selectedPoliza.fechaEmision).toLocaleDateString('es-ES')}
                      </p>
                    </div>
                    <div>
                      <p className="text-sm text-slate-600">Suma Asegurada</p>
                      <p className="text-lg font-semibold text-slate-900">
                        {formatCurrency(selectedPoliza.sumaAsegurada)}
                      </p>
                    </div>
                  </div>
                </div>

                <div className="border-b border-slate-200 pb-6">
                  <div className="grid grid-cols-2 gap-4">
                    <div>
                      <p className="text-sm text-slate-600">Cliente</p>
                      <p className="text-lg font-semibold text-slate-900">{selectedPoliza.cliente}</p>
                    </div>
                    <div>
                      <p className="text-sm text-slate-600">Vehiculo</p>
                      <p className="text-lg font-semibold text-slate-900">{selectedPoliza.vehiculo}</p>
                    </div>
                  </div>
                </div>

                <div className="bg-slate-50 rounded-xl p-6">
                  <div className="flex items-center justify-between">
                    <span className="text-lg font-semibold text-slate-900">Prima Total</span>
                    <span className="text-3xl font-bold text-[#43bb6c]">
                      {formatCurrency(selectedPoliza.primaTotal)}
                    </span>
                  </div>
                </div>
              </div>

              <div className="mt-6">
                <button
                  onClick={handleCloseModal}
                  className="w-full rounded-xl bg-slate-900 px-4 py-3 font-medium text-white hover:bg-slate-800 transition-colors"
                >
                  Cerrar
                </button>
              </div>
            </div>
          </div>
        </>
      )}
    </>
  );
}

import { useMemo, useState } from 'react';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { emitirPoliza, getClientes, getCoberturas } from '../api/insuranceApi';
import { formatCurrency } from '../lib/utils';
import type { EmitirPolizaRequest } from '../types/api';

export function NewPolicyPage() {
  const queryClient = useQueryClient();

  const [clienteId, setClienteId] = useState(0);
  const [placa, setPlaca] = useState('');
  const [marca, setMarca] = useState('');
  const [modelo, setModelo] = useState('');
  const [anio, setAnio] = useState(new Date().getFullYear());
  const [valorComercial, setValorComercial] = useState(0);
  const [coberturasIds, setCoberturasIds] = useState<number[]>([]);

  const clientesQuery = useQuery({
    queryKey: ['clientes'],
    queryFn: getClientes
  });

  const coberturasQuery = useQuery({
    queryKey: ['coberturas'],
    queryFn: getCoberturas
  });

  const selectedCoverages = useMemo(() => {
    return coberturasQuery.data?.filter((item) => coberturasIds.includes(item.id)) ?? [];
  }, [coberturasIds, coberturasQuery.data]);

  const primaEstimada = selectedCoverages.reduce(
    (total, item) => total + item.montoCobertura,
    0
  );

  const mutation = useMutation({
    mutationFn: emitirPoliza,
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: ['polizas'] });
      setPlaca('');
      setMarca('');
      setModelo('');
      setAnio(new Date().getFullYear());
      setValorComercial(0);
      setCoberturasIds([]);
    }
  });

  function toggleCobertura(id: number) {
    setCoberturasIds((current) =>
      current.includes(id)
        ? current.filter((item) => item !== id)
        : [...current, id]
    );
  }

  function handleSubmit(event: React.FormEvent<HTMLFormElement>) {
    event.preventDefault();

    const payload: EmitirPolizaRequest = {
      clienteId,
      vehiculo: {
        placa,
        marca,
        modelo,
        anio,
        valorComercial
      },
      coberturasIds
    };

    mutation.mutate(payload);
  }

  return (
    <section className="rounded-2xl border border-slate-200 bg-white p-6 shadow-sm">
      <h2 className="text-xl font-semibold text-slate-900">Nueva póliza</h2>
      <p className="mt-1 text-sm text-slate-500">
        Captura cliente, vehículo y coberturas para emitir una póliza.
      </p>

      <form onSubmit={handleSubmit} className="mt-6 grid gap-5">
        <div>
          <label className="text-sm font-medium text-slate-700">Cliente</label>
          <select
            className="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2"
            value={clienteId}
            onChange={(event) => setClienteId(Number(event.target.value))}
            required
          >
            <option value={0}>Seleccione un cliente</option>
            {clientesQuery.data?.map((cliente) => (
              <option key={cliente.id} value={cliente.id}>
                {cliente.nombre} - {cliente.identificacion}
              </option>
            ))}
          </select>
        </div>

        <div className="grid gap-4 md:grid-cols-2">
          <Input label="Placa" value={placa} onChange={setPlaca} />
          <Input label="Marca" value={marca} onChange={setMarca} />
          <Input label="Modelo" value={modelo} onChange={setModelo} />
          <Input
            label="Año"
            type="number"
            value={String(anio)}
            onChange={(value) => setAnio(Number(value))}
          />
          <Input
            label="Valor comercial"
            type="number"
            value={String(valorComercial)}
            onChange={(value) => setValorComercial(Number(value))}
          />
        </div>

        <div>
          <h3 className="text-sm font-medium text-slate-700">Coberturas</h3>
          <div className="mt-2 grid gap-3 md:grid-cols-2">
            {coberturasQuery.data?.map((cobertura) => {
              const isSelected = coberturasIds.includes(cobertura.id);
              return (
                <label
                  key={cobertura.id}
                  className={`flex cursor-pointer items-start gap-3 rounded-xl border p-4 transition-colors ${
                    isSelected
                      ? 'border-[#0f6137] bg-[#0f6137]'
                      : 'border-slate-200 hover:bg-[#E8FDE8]'
                  }`}
                >
                  <input
                    type="checkbox"
                    checked={isSelected}
                    onChange={() => toggleCobertura(cobertura.id)}
                    className="appearance-none mt-1"
                  />
                  <span>
                    <span className={`block font-medium ${isSelected ? 'text-white' : 'text-slate-900'}`}>
                      {cobertura.nombre}
                    </span>
                    <span className={`block text-sm ${isSelected ? 'text-white/80' : 'text-slate-500'}`}>
                      {formatCurrency(cobertura.montoCobertura)}
                    </span>
                  </span>
                </label>
              );
            })}
          </div>
        </div>

        <div className="rounded-xl bg-slate-50 p-4">
          <p className="text-sm text-slate-500">Prima estimada</p>
          <p className="text-2xl font-bold text-slate-900">{formatCurrency(primaEstimada)}</p>
        </div>

        {mutation.isError && (
          <p className="rounded-xl bg-red-50 p-3 text-sm text-red-700">
            No se pudo emitir la póliza. Revisá los datos ingresados.
          </p>
        )}

        {mutation.isSuccess && (
          <p className="rounded-xl bg-green-50 p-3 text-sm text-green-700">
            Póliza emitida correctamente.
          </p>
        )}

        <button
          type="submit"
          disabled={mutation.isPending}
          className="rounded-xl bg-[#0f6137] px-4 py-3 font-medium text-white disabled:opacity-60"
        >
          {mutation.isPending ? 'Emitiendo...' : 'Emitir póliza'}
        </button>
      </form>
    </section>
  );
}

type InputProps = {
  label: string;
  value: string;
  type?: string;
  onChange: (value: string) => void;
};

function Input({ label, value, type = 'text', onChange }: InputProps) {
  return (
    <div>
      <label className="text-sm font-medium text-slate-700">{label}</label>
      <input
        type={type}
        className="mt-1 w-full rounded-xl border border-slate-300 px-3 py-2"
        value={value}
        onChange={(event) => onChange(event.target.value)}
        required
      />
    </div>
  );
}

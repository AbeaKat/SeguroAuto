import { useState } from 'react';
import { CarFront, FilePlus2, History } from 'lucide-react';
import { NewPolicyPage } from './pages/NewPolicyPage';
import { PoliciesPage } from './pages/PoliciesPage';

type Tab = 'new' | 'history';

export default function App() {
  const [tab, setTab] = useState<Tab>('new');

  return (
    <main className="min-h-screen bg-slate-50 px-4 py-8 text-slate-900">
      <div className="mx-auto max-w-6xl">
              <header className="mb-8 rounded-3xl bg-[#0f6137] p-8 text-white shadow-sm">
          <div className="flex items-center gap-3">
            <div className="rounded-2xl bg-white/10 p-3">
              <CarFront />
            </div>
            <div>
              <h1 className="text-3xl font-bold">Sistema de Emisión de Seguros de Auto</h1>
              <p className="mt-1 text-slate-300">
                Emisión y consulta de pólizas.
              </p>
            </div>
          </div>
        </header>

        <nav className="mb-6 flex gap-3">
          <button
            onClick={() => setTab('new')}
            className={`flex items-center gap-2 rounded-xl px-4 py-2 font-medium ${
              tab === 'new'
                ? 'bg-[#0f6137] text-white'
                : 'border border-slate-200 bg-white text-slate-700'
            }`}
          >
            <FilePlus2 size={18} />
            Nueva póliza
          </button>
          <button
            onClick={() => setTab('history')}
            className={`flex items-center gap-2 rounded-xl px-4 py-2 font-medium ${
              tab === 'history'
                ? 'bg-[#0f6137] text-white'
                : 'border border-slate-200 bg-white text-slate-700'
            }`}
          >
            <History size={18} />
            Historial
          </button>
        </nav>

        {tab === 'new' ? <NewPolicyPage /> : <PoliciesPage />}
      </div>
    </main>
  );
}

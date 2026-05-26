import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar';
import Dashboard from './pages/Dashboard';
import Members from './pages/Members';
import Trainers from './pages/Trainers';
import Classes from './pages/Classes';
import Memberships from './pages/Memberships';
import Enrollments from './pages/Enrollments';

export default function App() {
  return (
    <BrowserRouter>
      <Navbar />
      <main>
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/members" element={<Members />} />
          <Route path="/trainers" element={<Trainers />} />
          <Route path="/classes" element={<Classes />} />
          <Route path="/memberships" element={<Memberships />} />
          <Route path="/enrollments" element={<Enrollments />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}

import React, { useState } from 'react';
import type { ApiError } from '../interfaces/ApiError';

interface CreatePartModalProps {
  show: boolean;
  onClose: () => void;
  onCreated?: () => void;
}

export function CreatePartModal({ show, onClose, onCreated }: CreatePartModalProps) {
  const [partNumber, setPartNumber] = useState('');
  const [description, setDescription] = useState('');
  const [quantityOnHand, setQuantityOnHand] = useState(0);
  const [locationCode, setLocationCode] = useState('');
  const [error, setError] = useState<ApiError | null>(null);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError(null);

    try {
      const response = await fetch('https://localhost:5001/api/parts', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          partNumber,
          description,
          quantityOnHand,
          locationCode
        })
      });

      if (!response.ok) {
        const data: ApiError = await response.json();
        setError(data);
        return;
      }

      setPartNumber('');
      setDescription('');
      setQuantityOnHand(0);
      setLocationCode('');
      if (onCreated) onCreated();
      onClose();
    } catch (err: any) {
      setError({
        type: 'exception',
        title: 'Unexpected error',
        status: 0,
        detail: err.message || 'Unknown error',
        errors: []
      });
    } finally {
      setLoading(false);
    }
  };

  if (!show) return null;

  return (
    <div className="modal show" style={{ display: 'block' }} tabIndex={-1} id="addPartModal">
      <div className="modal-dialog">
        <div className="modal-content">
          <form onSubmit={handleSubmit}>
            <div className="modal-header">
              <h5 className="modal-title">Create Part</h5>
              <button type="button" className="btn-close" onClick={onClose} aria-label="Close"></button>
            </div>
            <div className="modal-body">
              {error && (
                <div className="alert alert-danger">
                  {error.detail && <div><strong>{error.detail}</strong></div>}
                  {error.errors && error.errors.length > 0 && (
                    <ul className="mb-0">
                      {error.errors.map((err, idx) => (
                        <li key={idx}>{err.description}</li>
                      ))}
                    </ul>
                  )}
                </div>
              )}
              <div className="mb-3">
                <label className="form-label">Part Number</label>
                <input className="form-control" value={partNumber} onChange={e => setPartNumber(e.target.value)} required />
              </div>
              <div className="mb-3">
                <label className="form-label">Description</label>
                <input className="form-control" value={description} onChange={e => setDescription(e.target.value)} required />
              </div>
              <div className="mb-3">
                <label className="form-label">Quantity On Hand</label>
                <input type="number" className="form-control" value={quantityOnHand} onChange={e => setQuantityOnHand(Number(e.target.value))} required />
              </div>
              <div className="mb-3">
                <label className="form-label">Location Code</label>
                <input className="form-control" value={locationCode} onChange={e => setLocationCode(e.target.value)} required />
              </div>
            </div>
            <div className="modal-footer">
              <button type="button" className="btn btn-secondary" onClick={onClose}>Close</button>
              <button type="submit" className="btn btn-primary" disabled={loading}>
                {loading ? 'Saving...' : 'Save changes'}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
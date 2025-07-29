import React, { useState, useEffect } from 'react';
import type { Part } from '../interfaces/Part';

interface DeletePartModalProps {
  part: Part | null;
  show: boolean;
  onClose: () => void;
  onDeleted: () => void;
}

export function DeletePartModal({ part, show, onClose, onDeleted }: DeletePartModalProps) {
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    if (show) setError(null);
  }, [show, part]);

  if (!show || !part) return null;

  const handleDelete = async () => {
    setLoading(true);
    setError(null);
    try {
      const response = await fetch(`https://localhost:5001/api/parts/${encodeURIComponent(part.partNumber)}`, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json' }
      });
      if (!response.ok) {
        const data = await response.json();
        setError(data?.title || 'Failed to delete part');
        return;
      }
      onDeleted();
      onClose();
    } catch (err: any) {
      setError(err.message || 'Unexpected error');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal show" style={{ display: 'block' }} tabIndex={-1}>
      <div className="modal-dialog">
        <div className="modal-content">
          <div className="modal-header">
            <h5 className="modal-title">Delete Part</h5>
            <button type="button" className="btn-close" onClick={onClose} aria-label="Close"></button>
          </div>
          <div className="modal-body">
            {error && <div className="alert alert-danger">{error}</div>}
            <p>Are you sure you want to delete part <strong>{part.partNumber}</strong>?</p>
          </div>
          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={onClose}>Cancel</button>
            <button type="button" className="btn btn-danger" onClick={handleDelete} disabled={loading}>
              {loading ? 'Deleting...' : 'Delete'}
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
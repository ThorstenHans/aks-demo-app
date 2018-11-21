import { Observable } from 'rxjs';

export abstract class ExportService {
  public abstract exportSession(sessionId: string): Observable<any>;
}

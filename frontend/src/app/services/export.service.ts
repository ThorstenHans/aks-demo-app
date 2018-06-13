import { Observable } from 'rxjs/Observable';

export abstract class ExportService {
  public abstract generatePdf(sessionId: string, mail: string): Observable<any>;
}

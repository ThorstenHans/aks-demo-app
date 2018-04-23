import { Voting } from './voting';
export class VotingSummary {
  public sessionId: string;
  public downVotes: number;
  public upVotes: number;

  public static fromVotingsArray(sessionId: string, votings: Array<Voting>): VotingSummary {
    if (!votings) {
      throw new Error(`Can't create VotingSummary from nothing`);
    }
    const instance = new VotingSummary();
    instance.sessionId = sessionId;
    instance.upVotes = votings.filter(v => v.value > 0).reduce((prev, current) => prev + current.value, 0);
    instance.downVotes = votings.filter(v => v.value < 0).reduce((prev, current) => prev + current.value, 0);
    return instance;
  }
}

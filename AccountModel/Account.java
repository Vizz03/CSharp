public class Account {

    public static class InsufficientFundsException extends Exception {
    }

    private int balance;

    public synchronized void transferTo(final int amount, final Account destination) throws InsufficientFundsException {
        synchronized (destination) {
            withdraw(amount);
            destination.deposit(amount);
        }
    }

    public synchronized void withdraw(int amount) throws InsufficientFundsException {
        if (sufficientFunds(amount)) {
            balance -= amount;
        } else {
            throw new InsufficientFundsException();
        }
    }

    public synchronized void deposit(int amount) {
        balance += amount;
    }

    public synchronized int balance() {
        return balance;
    }

    private boolean sufficientFunds(int amount) {
        return balance >= amount;
    }

}

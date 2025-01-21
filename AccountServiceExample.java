// 使用受檢異常的方式（較不理想的方法）
class TraditionalAccountService {
    // 注意這裡必須聲明所有可能拋出的受檢異常
    public BigDecimal getBalance(String accountId) 
            throws AccountNotFoundException, 
                   DatabaseException, 
                   NetworkException {
        validateAccount(accountId);
        return fetchBalanceFromDatabase(accountId);
    }

    private void validateAccount(String accountId) 
            throws AccountNotFoundException {
        if (accountId == null || accountId.trim().isEmpty()) {
            throw new AccountNotFoundException("Account ID cannot be empty");
        }
        // 其他驗證邏輯...
    }

    private BigDecimal fetchBalanceFromDatabase(String accountId) 
            throws DatabaseException, NetworkException {
        try {
            // 數據庫操作...
            return BigDecimal.ZERO;
        } catch (SQLException e) {
            throw new DatabaseException("Database error", e);
        } catch (IOException e) {
            throw new NetworkException("Network error", e);
        }
    }
}

// 使用非受檢異常的方式（更好的方法）
class ModernAccountService {
    public BigDecimal getBalance(String accountId) {
        if (accountId == null || accountId.trim().isEmpty()) {
            throw new IllegalArgumentException("Account ID cannot be empty");
        }

        Account account = findAccount(accountId);
        return account.getBalance();
    }

    private Account findAccount(String accountId) {
        try {
            // 數據庫操作...
            throw new AccountNotFoundException("Account not found: " + accountId);
        } catch (Exception e) {
            // 轉換為運行時異常
            throw new AccountOperationException("Error finding account", e);
        }
    }
}

// 受檢異常
class AccountNotFoundException extends Exception {
    public AccountNotFoundException(String message) {
        super(message);
    }
}

class DatabaseException extends Exception {
    public DatabaseException(String message, Throwable cause) {
        super(message, cause);
    }
}

class NetworkException extends Exception {
    public NetworkException(String message, Throwable cause) {
        super(message, cause);
    }
}

// 非受檢異常（運行時異常）
class AccountOperationException extends RuntimeException {
    public AccountOperationException(String message, Throwable cause) {
        super(message, cause);
    }
}

class Account {
    private BigDecimal balance;
    
    public BigDecimal getBalance() {
        return balance;
    }
} 
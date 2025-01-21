namespace CleanCodeProject.C07;

public class DefineExceptionInTermsOfACallersNeeds
{
    public void UseAPIExceptionExample()
    {
        ACMEPort port = new ACMEPort(12);
        try
        {
            port.Open();
        }
        catch (DeviceResponseException e)
        {
            reportPortError(e);
            logger.log("Device response exception", e);
        }
        catch (ATM1212UnlockedException e)
        {
            reportPortError(e);
            logger.log("Unlock exception", e);
        }
        catch (GMXError e)
        {
            reportPortError(e);
            logger.log("Device response exception");
        }
        finally
        {
            // …
        }
    }

    public void UseLocalExceptionExample()
    {
        LocalPort port = new LocalPort(12);
        try
        {
            port.Open();
        }
        catch (PortDeviceFailure e)
        {
            reportError(e);
            logger.log(e.Message, e);
        }
        finally
        {
            // …
        }
    }
}

public class LocalPort
{
    private readonly ACMEPort _innerPort;
    public LocalPort(int portNumber)
    {
        _innerPort = new ACMEPort(portNumber);
    }
    public void Open()
    {
        try
        {
            _innerPort.Open();
        }
        catch (DeviceResponseException e)
        {
            throw new PortDeviceFailure(e);
        }
        catch (ATM1212UnlockedException e)
        {
            throw new PortDeviceFailure(e);
        }
        catch (GMXError e)
        {
            throw new PortDeviceFailure(e);
        }
    }
    // …
}
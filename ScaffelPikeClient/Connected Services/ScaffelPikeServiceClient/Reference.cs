﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScaffelPikeClient.ScaffelPikeServiceClient {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ScaeffelPike.Service", ConfigurationName="ScaffelPikeServiceClient.IScaffelPikeService")]
    public interface IScaffelPikeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ScaeffelPike.Service/IScaffelPikeService/LogIn", ReplyAction="http://ScaeffelPike.Service/IScaffelPikeService/LogInResponse")]
        ScaffelPikeLib.PasswordDto LogIn(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ScaeffelPike.Service/IScaffelPikeService/LogIn", ReplyAction="http://ScaeffelPike.Service/IScaffelPikeService/LogInResponse")]
        System.Threading.Tasks.Task<ScaffelPikeLib.PasswordDto> LogInAsync(string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IScaffelPikeServiceChannel : ScaffelPikeClient.ScaffelPikeServiceClient.IScaffelPikeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ScaffelPikeServiceClient : System.ServiceModel.ClientBase<ScaffelPikeClient.ScaffelPikeServiceClient.IScaffelPikeService>, ScaffelPikeClient.ScaffelPikeServiceClient.IScaffelPikeService {
        
        public ScaffelPikeServiceClient() {
        }
        
        public ScaffelPikeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ScaffelPikeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ScaffelPikeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ScaffelPikeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public ScaffelPikeLib.PasswordDto LogIn(string username, string password) {
            return base.Channel.LogIn(username, password);
        }
        
        public System.Threading.Tasks.Task<ScaffelPikeLib.PasswordDto> LogInAsync(string username, string password) {
            return base.Channel.LogInAsync(username, password);
        }
    }
}
variable "service_principal_id" {
    type = "string"
}

variable "service_principal_password" {
    type = "string"
}

variable "ssh_public_key" {
    type = "string"
}

variable "resource_group_name" {
    type = "string"
}

variable "aks_resource_group_name" {
    type = "string"
}

variable "responsible_name" {
    type = "string"
}

variable "acr_name" {
    type = "string"
}

variable "acr_sku" {
    type = "string"
    default = "Basic"
}

variable "aks_name" {
    type = "string"
}

variable "aks_agent_vm_size" {
    type = "string"
    default = "Standard_D2_v2"
}

variable "aks_agent_vm_count" {
    type = "string"
    default = "2"
}

variable "k8s_version" {
    type = "string"
    default = "1.9.6"
}

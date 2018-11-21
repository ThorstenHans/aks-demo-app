variable "service_principal_id" {
  type = "string"
}

variable "service_principal_password" {
  type = "string"
}

variable "ssh_public_key" {
  type = "string"
}

variable "aks_dns_prefix" {
  type = "string"
}

variable "resource_group_name" {
  type = "string"
}

variable "tags" {
  type = "map"

  default = {
    Environment = "Demo"
    Responsible = "Thorsten Hans"
  }
}

variable "acr_name" {
  type = "string"
}

variable "acr_sku" {
  type    = "string"
  default = "Standard"
}

variable "aks_name" {
  type = "string"
}

variable "aks_agent_vm_size" {
  type    = "string"
  default = "Standard_D2_v2"
}

variable "aks_agent_vm_count" {
  type    = "string"
  default = "2"
}

variable "k8s_version" {
  type    = "string"
  default = "1.9.6"
}

variable "sendgrid_key" {
  type = "string"
}

variable "appinsights_key" {
  type = "string"
}

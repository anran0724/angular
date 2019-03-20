namespace Sinodom.ElevatorCloud.Web.Areas.AppAreaName.Models.Common
{
    public class EditionPermissionTreeItemModel
    {
        public IEditionPermissionsEditViewModel EditModel { get; set; }

        public string ParentName { get; set; }

        public EditionPermissionTreeItemModel()
        {

        }

        public EditionPermissionTreeItemModel(IEditionPermissionsEditViewModel editModel, string parentName)
        {
            EditModel = editModel;
            ParentName = parentName;
        }
    }
}
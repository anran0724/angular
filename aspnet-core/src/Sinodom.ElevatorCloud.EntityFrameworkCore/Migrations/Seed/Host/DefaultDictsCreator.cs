namespace Sinodom.ElevatorCloud.Migrations.Seed.Host
{
    using System.Collections.Generic;
    using System.Linq;
    using Sinodom.ElevatorCloud.ECCPBaseAreas;
    using Sinodom.ElevatorCloud.ECCPBaseCommunities;
    using Sinodom.ElevatorCloud.EccpDict;
    using Sinodom.ElevatorCloud.EntityFrameworkCore;

    public class DefaultDictsCreator
    {
        private readonly ElevatorCloudDbContext _context;

        public DefaultDictsCreator(ElevatorCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            this.CreateElevatorStatuses();
            this.CreateEccpDictElevatorTypes();
            this.CreateEccpDictMaintenanceStatus();
            this.CreateEccpDictMaintenanceTypes();
            this.CreateEccpDictMaintenanceWorkFlowStatus();
            this.CreateEccpDictNodeTypes();
            this.CreateEccpDictPlaceTypes();
            this.CreateEccpDictTempWorkOrderTypes();
            this.CreateEccpDictMaintenanceItems();
            this.CreateEccpDictWorkOrderTypes();
            this.CreateEccpDictLabelStatus();
        }

        private void CreateElevatorStatuses()
        {
            var defaultElevatorStatuses = new List<string> { "屏蔽", "测试", "停用", "启用" };
            foreach (string elevatorStatus in defaultElevatorStatuses)
            {
                if (this._context.ECCPDictElevatorStatuses.Any(e => e.Name == elevatorStatus) == false)
                {
                    this._context.ECCPDictElevatorStatuses.Add(new ECCPDictElevatorStatus { Name = elevatorStatus });
                    this._context.SaveChanges();
                }
            }
        }

        private void CreateEccpDictElevatorTypes()
        {
            var defaultEccpDictElevatorTypes = new List<string>
                                                   {
                                                       "载货电梯",
                                                       "医梯",
                                                       "乘货电梯",
                                                       "无机房曳引式病床客梯",
                                                       "无机房曳引式观光电梯",
                                                       "液压乘客电梯",
                                                       "曳引乘客电梯",
                                                       "病床电梯",
                                                       "观光电梯",
                                                       "观光梯",
                                                       "有机房曳引式病床客梯",
                                                       "载客电梯",
                                                       "医用梯",
                                                       "客运电梯",
                                                       "3400",
                                                       "3200",
                                                       "3300",
                                                       "3100",
                                                       "曳引式客梯",
                                                       "客梯",
                                                       "NULL",
                                                       "无机房客梯	",
                                                       "货梯",
                                                       "杂物电梯",
                                                       "医用电梯",
                                                       "曳引式货梯",
                                                       "曳引式杂物梯",
                                                       "病床客梯",
                                                       "液压货梯",
                                                       "曳引式乘客电梯",
                                                       "EL",
                                                       "无机房曳引式客梯",
                                                       "液压电梯",
                                                       "无机房曳引式病床电梯",
                                                       "有机房曳引机病床电梯",
                                                       "乘客电梯",
                                                       "有机房曳引式客梯",
                                                       "有机房曳引式病床电梯",
                                                       "曳引",
                                                       "液压",
                                                       "强制",
                                                       "无",
                                                       "曳引",
                                                       "液压驱动电梯",
                                                       "曳引与强制驱动电梯",
                                                       "其它类型电梯",
                                                       "自动扶梯与自动人行道"
                                                   };
            foreach (string elevatorType in defaultEccpDictElevatorTypes)
            {
                if (this._context.EccpDictElevatorTypes.Any(e => e.Name == elevatorType) == false)
                {
                    this._context.EccpDictElevatorTypes.Add(new EccpDictElevatorType { Name = elevatorType });
                    this._context.SaveChanges();
                }
            }
        }

        private void CreateEccpDictMaintenanceStatus()
        {
            var defaultMaintenanceStatuses = new List<string> { "未进行", "进行中", "已完成", "已超期" };
            foreach (string maintenanceStatus in defaultMaintenanceStatuses)
            {
                if (this._context.EccpDictMaintenanceStatuses.Any(e => e.Name == maintenanceStatus) == false)
                {
                    this._context.EccpDictMaintenanceStatuses.Add(new EccpDictMaintenanceStatus { Name = maintenanceStatus });
                    this._context.SaveChanges();
                }
            }
        }

        private void CreateEccpDictMaintenanceTypes()
        {
            var defaultEccpDictMaintenanceTypes = new List<string> { "半月维保", "月度维保", "季度度保", "半年维保", "年度维保" };
            foreach (string maintenanceType in defaultEccpDictMaintenanceTypes)
            {
                if (this._context.EccpDictMaintenanceTypes.Any(e => e.Name == maintenanceType) == false)
                {
                    this._context.EccpDictMaintenanceTypes.Add(new EccpDictMaintenanceType { Name = maintenanceType });
                    this._context.SaveChanges();
                }
            }
        }

        private void CreateEccpDictMaintenanceWorkFlowStatus()
        {
            var defaultEccpDictMaintenanceWorkFlowStatus = new List<string> { "进行中", "已完成" };
            foreach (string maintenanceWorkFlowStatus in defaultEccpDictMaintenanceWorkFlowStatus)
            {
                if (this._context.EccpDictMaintenanceWorkFlowStatuses.Any(e => e.Name == maintenanceWorkFlowStatus) == false)
                {
                    this._context.EccpDictMaintenanceWorkFlowStatuses.Add(new EccpDictMaintenanceWorkFlowStatus { Name = maintenanceWorkFlowStatus });
                    this._context.SaveChanges();
                }
            }
        }

        private void CreateEccpDictNodeTypes()
        {
            var defaultEccpDictNodeTypes = new List<string> { "判断节点", "分支节点", "描述节点", "操作节点" };
            foreach (string eccpDictNodeType in defaultEccpDictNodeTypes)
            {
                if (this._context.EccpDictNodeTypes.Any(e => e.Name == eccpDictNodeType) == false)
                {
                    this._context.EccpDictNodeTypes.Add(new EccpDictNodeType { Name = eccpDictNodeType });
                    this._context.SaveChanges();
                }
            }
        }


        private void CreateEccpDictPlaceTypes()
        {
            var defaultEccpDictPlaceTypes = new List<string>
                                                {
                                                    "住宅",
                                                    "办公楼宇",
                                                    "商场超市",
                                                    "宾馆饭店",
                                                    "交通场所",
                                                    "医院",
                                                    "学校",
                                                    "文体娱场馆",
                                                    "其他场所"
                                                };
            foreach (string eccpDictPlaceType in defaultEccpDictPlaceTypes)
            {
                if (this._context.EccpDictPlaceTypes.Any(e => e.Name == eccpDictPlaceType) == false)
                {
                    this._context.EccpDictPlaceTypes.Add(new EccpDictPlaceType { Name = eccpDictPlaceType });
                    this._context.SaveChanges();
                }
            }
        }

        private void CreateEccpDictTempWorkOrderTypes()
        {
            var defaultEccpDictTempWorkOrderTypes = new List<string>
                                                {
                                                    "救援工单",
                                                    "维修工单"
                                                };
            foreach (string eccpDictTempWorkOrderType in defaultEccpDictTempWorkOrderTypes)
            {
                if (this._context.EccpDictTempWorkOrderTypes.Any(e => e.Name == eccpDictTempWorkOrderType) == false)
                {
                    this._context.EccpDictTempWorkOrderTypes.Add(new EccpDictTempWorkOrderType { Name = eccpDictTempWorkOrderType });
                    this._context.SaveChanges();
                }
            }
        }

        private void CreateEccpDictWorkOrderTypes()
        {
            var defaultEccpDictWorkOrderTypes = new List<string>
                                                {
                                                    "维保工单",
                                                    "临时工单"
                                                };
            foreach (string eccpDictWorkOrderType in defaultEccpDictWorkOrderTypes)
            {
                if (this._context.EccpDictWorkOrderTypes.Any(e => e.Name == eccpDictWorkOrderType) == false)
                {
                    this._context.EccpDictWorkOrderTypes.Add(new EccpDictWorkOrderType { Name = eccpDictWorkOrderType });
                    this._context.SaveChanges();
                }
            }
        }

        private void CreateEccpDictMaintenanceItems()
        {
            var defaultEccpDictMaintenanceItems = new List<EccpDictMaintenanceItem>
                                                      {
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "制动器作为轿厢意外移动保护装置制停子系统时的自监测",
                                                                  TermCode = "6",
                                                                  DisOrder = 6,
                                                                  TermDesc = "制动力人工方式检测符合使用维护说明书要求；制动力自监测系统有记录"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "消防开关",
                                                                  TermCode = "42",
                                                                  DisOrder = 42,
                                                                  TermDesc = "工作正常，功能有效"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "上、下极限开关",
                                                                  TermCode = "59",
                                                                  DisOrder = 59,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "导电回路绝缘性能测试",
                                                                  TermCode = "64",
                                                                  DisOrder = 64,
                                                                  TermDesc = "符合标准"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name =
                                                                      "限速器安全钳联动实验（对于使用年限不超过15年的限速器，每2年进行一次限速器动作速度校验；对于使用年限超过15年的限速器，每年进行一次限速器动作速度校验）",
                                                                  TermCode = "65",
                                                                  DisOrder = 65,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "上行超速保护装置动作实验",
                                                                  TermCode = "66",
                                                                  DisOrder = 66,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿厢意外移动保护装置动作实验",
                                                                  TermCode = "67",
                                                                  DisOrder = 67,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿门防撞击保护装置（安全触板、光幕、光电等）",
                                                                  TermCode = "20",
                                                                  DisOrder = 20,
                                                                  TermDesc = "功能有效"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿门门锁电气触点",
                                                                  TermCode = "21",
                                                                  DisOrder = 21,
                                                                  TermDesc = "清洁，触点接触良好，接线可靠"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿门运行",
                                                                  TermCode = "22",
                                                                  DisOrder = 22,
                                                                  TermDesc = "开启和关闭工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "验证轿门关闭的电气安全装置",
                                                                  TermCode = "39",
                                                                  DisOrder = 39,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门、轿门系统中传动钢丝绳、链条、传动带",
                                                                  TermCode = "40",
                                                                  DisOrder = 40,
                                                                  TermDesc = "按照制造单位要求清洁、调整"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门、轿门门扇",
                                                                  TermCode = "55",
                                                                  DisOrder = 55,
                                                                  TermDesc = "门扇各相关间隙符合标准值"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿门开门限制装置",
                                                                  TermCode = "56",
                                                                  DisOrder = 56,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿厢照明、风扇、应急照明",
                                                                  TermCode = "16",
                                                                  DisOrder = 16,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿厢检修开关、停止装置",
                                                                  TermCode = "17",
                                                                  DisOrder = 17,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿内报警装置、对讲系统",
                                                                  TermCode = "18",
                                                                  DisOrder = 18,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿内显示、指令按钮、IC卡系统",
                                                                  TermCode = "19",
                                                                  DisOrder = 19,
                                                                  TermDesc = "齐全、有效"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿厢平层准确度",
                                                                  TermCode = "23",
                                                                  DisOrder = 23,
                                                                  TermDesc = "符合标准值"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "选层器动静触点",
                                                                  TermCode = "35",
                                                                  DisOrder = 35,
                                                                  TermDesc = "清洁，无烧蚀"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿厢称重装置",
                                                                  TermCode = "73",
                                                                  DisOrder = 73,
                                                                  TermDesc = "准确有效"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "安全钳钳座",
                                                                  TermCode = "74",
                                                                  DisOrder = 74,
                                                                  TermDesc = "固定无松动"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿底各安装螺栓",
                                                                  TermCode = "75",
                                                                  DisOrder = 75,
                                                                  TermDesc = "紧固"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿顶",
                                                                  TermCode = "11",
                                                                  DisOrder = 11,
                                                                  TermDesc = "干净整洁，防护栏无锈蚀，松动，脱落"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿顶检修开关、停止装置",
                                                                  TermCode = "12",
                                                                  DisOrder = 12,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "导靴上油杯",
                                                                  TermCode = "13",
                                                                  DisOrder = 13,
                                                                  TermDesc = "无泄漏，油量适宜，吸油毛毡齐全"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "对重/平衡重块及其压板",
                                                                  TermCode = "14",
                                                                  DisOrder = 14,
                                                                  TermDesc = "对重/平衡重块及其压板无松动，压板坚固"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "井道照明",
                                                                  TermCode = "15",
                                                                  DisOrder = 15,
                                                                  TermDesc = "齐全，正常有效"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "靴衬、滚轮",
                                                                  TermCode = "38",
                                                                  DisOrder = 38,
                                                                  TermDesc = "清洁，磨损量不超过制造单位要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿顶、轿厢架、轿门及其附件安装螺栓",
                                                                  TermCode = "68",
                                                                  DisOrder = 68,
                                                                  TermDesc = "紧固"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿厢和对重/平衡重的导轨支架",
                                                                  TermCode = "69",
                                                                  DisOrder = 69,
                                                                  TermDesc = "固定，无松动"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "轿厢和对重/平衡重的导轨",
                                                                  TermCode = "70",
                                                                  DisOrder = 70,
                                                                  TermDesc = "清洁，压板牢固"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "随行电缆",
                                                                  TermCode = "71",
                                                                  DisOrder = 71,
                                                                  TermDesc = "无损伤"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "机房、滑轮间环境",
                                                                  TermCode = "1",
                                                                  DisOrder = 1,
                                                                  TermDesc = "干净、整洁，门窗完好，照明正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "手动紧急操作装置",
                                                                  TermCode = "2",
                                                                  DisOrder = 2,
                                                                  TermDesc = "齐全，放置在指定的位置"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "曳引驱动主机",
                                                                  TermCode = "3",
                                                                  DisOrder = 3,
                                                                  TermDesc = "运行时无异常震动和异常声响"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "制动器各销轴部位",
                                                                  TermCode = "4",
                                                                  DisOrder = 4,
                                                                  TermDesc = "进行润滑，保证其动作灵活"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "制动器间隙",
                                                                  TermCode = "5",
                                                                  DisOrder = 5,
                                                                  TermDesc = "在打开时制动衬与制动轮无摩擦，间隙值符合制造单位要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "编码器",
                                                                  TermCode = "7",
                                                                  DisOrder = 7,
                                                                  TermDesc = "安装牢固，干净"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "限速器各销轴部位",
                                                                  TermCode = "8",
                                                                  DisOrder = 8,
                                                                  TermDesc = "进行润滑，保证其转动灵活，电气开关正常有效"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门和轿门旁路装置",
                                                                  TermCode = "9",
                                                                  DisOrder = 9,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "紧急电动运行",
                                                                  TermCode = "10",
                                                                  DisOrder = 10,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "减速机润滑油",
                                                                  TermCode = "32",
                                                                  DisOrder = 32,
                                                                  TermDesc = "油量适宜，除蜗杆伸出端外均无渗漏"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "制动衬",
                                                                  TermCode = "33",
                                                                  DisOrder = 33,
                                                                  TermDesc = "清洁，磨损量不超过制造单位要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "编码器",
                                                                  TermCode = "34",
                                                                  DisOrder = 34,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "曳引轮槽、悬挂装置",
                                                                  TermCode = "36",
                                                                  DisOrder = 36,
                                                                  TermDesc = "清洁，钢丝绳无严重油腻，张力均匀符合制造单位要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "限速器轮、限速器钢丝绳",
                                                                  TermCode = "37",
                                                                  DisOrder = 37,
                                                                  TermDesc = "清洁，无严重油腻"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "电动机与减速机联轴器",
                                                                  TermCode = "45",
                                                                  DisOrder = 45,
                                                                  TermDesc = "连接无松动，弹性元件外观良好，无老化等现象"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "驱动轮、导向轮轴承部",
                                                                  TermCode = "46",
                                                                  DisOrder = 46,
                                                                  TermDesc = "无异常声响，无振动，润滑良好"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "曳引轮槽",
                                                                  TermCode = "47",
                                                                  DisOrder = 47,
                                                                  TermDesc = "磨损量不超过制造单位要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "制动器动作状态监测装置",
                                                                  TermCode = "48",
                                                                  DisOrder = 48,
                                                                  TermDesc = "工作正常，制动器动作可靠"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "控制柜内各接线端子",
                                                                  TermCode = "49",
                                                                  DisOrder = 49,
                                                                  TermDesc = "各接线紧固、整齐、线号齐全清晰"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "控制柜各仪表",
                                                                  TermCode = "50",
                                                                  DisOrder = 50,
                                                                  TermDesc = "显示正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "悬挂装置、补偿绳",
                                                                  TermCode = "52",
                                                                  DisOrder = 52,
                                                                  TermDesc = "磨损量、断丝数不超过要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "绳头组合",
                                                                  TermCode = "53",
                                                                  DisOrder = 53,
                                                                  TermDesc = "螺母无松动"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "限速器钢丝绳",
                                                                  TermCode = "54",
                                                                  DisOrder = 54,
                                                                  TermDesc = "磨损量、断丝数不超过制造单位要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "减速机润滑油",
                                                                  TermCode = "60",
                                                                  DisOrder = 60,
                                                                  TermDesc = "按照制造单位要求适时更换，保证油质符合要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "控制柜接触器、继电器触点",
                                                                  TermCode = "61",
                                                                  DisOrder = 61,
                                                                  TermDesc = "接触良好"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "制动器铁芯（柱塞）",
                                                                  TermCode = "62",
                                                                  DisOrder = 62,
                                                                  TermDesc = "进行清洁、润滑、检查，磨损量不超过制造单位要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "制动器制动能力",
                                                                  TermCode = "63",
                                                                  DisOrder = 63,
                                                                  TermDesc =
                                                                      "符合制造单位要求，保持有足够的制动力，必要时进行轿厢装载125%额定载重量的自动实验"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "底坑环境",
                                                                  TermCode = "30",
                                                                  DisOrder = 30,
                                                                  TermDesc = "干净，无积水、渗水，地坑照明正常有效"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "底坑急停装置",
                                                                  TermCode = "31",
                                                                  DisOrder = 31,
                                                                  TermDesc = "开关正常有效"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "耗能缓冲器",
                                                                  TermCode = "43",
                                                                  DisOrder = 43,
                                                                  TermDesc = "电气安全装置功能有效，油量适中，柱塞无锈蚀"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "限速器涨紧轮装置盒和电气安全装置",
                                                                  TermCode = "44",
                                                                  DisOrder = 44,
                                                                  TermDesc = "工作正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "对重缓冲距离",
                                                                  TermCode = "57",
                                                                  DisOrder = 57,
                                                                  TermDesc = "符合标准值"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "补偿链（绳）与轿厢、对重结合处",
                                                                  TermCode = "58",
                                                                  DisOrder = 58,
                                                                  TermDesc = "固定，无松动"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "缓冲器",
                                                                  TermCode = "76",
                                                                  DisOrder = 76,
                                                                  TermDesc = "固定无松动"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层站召唤、层楼显示",
                                                                  TermCode = "24",
                                                                  DisOrder = 24,
                                                                  TermDesc = "齐全、有效"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门地坎",
                                                                  TermCode = "25",
                                                                  DisOrder = 25,
                                                                  TermDesc = "清洁"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门自动关门装置",
                                                                  TermCode = "26",
                                                                  DisOrder = 26,
                                                                  TermDesc = "正常"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门门锁自动复位",
                                                                  TermCode = "27",
                                                                  DisOrder = 27,
                                                                  TermDesc = "用层门钥匙打开手动开锁装置释放后，层门门锁能自动复位"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门门锁电气触点",
                                                                  TermCode = "28",
                                                                  DisOrder = 28,
                                                                  TermDesc = "进行清洁，其功能正常有效，接线可靠"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门锁紧元件啮合长度",
                                                                  TermCode = "29",
                                                                  DisOrder = 29,
                                                                  TermDesc = "不小于7mm"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门、轿门系统中传动钢丝绳、链条、传动带",
                                                                  TermCode = "40",
                                                                  DisOrder = 40,
                                                                  TermDesc = "按照制造单位要求清洁、调整"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门门导靴",
                                                                  TermCode = "41",
                                                                  DisOrder = 41,
                                                                  TermDesc = "磨损量不超过制造单位要求"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门、轿门门扇",
                                                                  TermCode = "55",
                                                                  DisOrder = 55,
                                                                  TermDesc = "门扇各相关间隙符合标准值"
                                                              },
                                                          new EccpDictMaintenanceItem
                                                              {
                                                                  Name = "层门装置和地坎",
                                                                  TermCode = "72",
                                                                  DisOrder = 72,
                                                                  TermDesc = "无影响正常使用的变形，各安装螺栓紧固"
                                                              }
                                                      };
            foreach (EccpDictMaintenanceItem eccpDictMaintenanceItem in defaultEccpDictMaintenanceItems)
            {
                if (this._context.EccpDictMaintenanceItems.Any(e => e.Name == eccpDictMaintenanceItem.Name) == false)
                {
                    this._context.EccpDictMaintenanceItems.Add(eccpDictMaintenanceItem);
                    this._context.SaveChanges();
                }
            }
        }

        private void CreateEccpDictLabelStatus()
        {
            var defaultEccpDictLabelStatuses = new List<string> { "未绑定", "已绑定", "已失效" };
            foreach (string labelStatus in defaultEccpDictLabelStatuses)
            {
                if (this._context.EccpDictLabelStatuses.Any(e => e.Name == labelStatus) == false)
                {
                    this._context.EccpDictLabelStatuses.Add(new EccpDictLabelStatus { Name = labelStatus });
                    this._context.SaveChanges();
                }
            }
        }
    }
}
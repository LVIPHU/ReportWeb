<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Top content -->
    <div class="top-content section-container" id="top-content">
        <div class="container">
            <div class="row">
                <div class="col col-md-10 offset-md-1 col-lg-8 offset-lg-2">
                    <h1 class="wow fadeIn">Tạo báo cáo động cùng <strong>Report Web</strong></h1>
                    <div class="description wow fadeInLeft">
                        <p>
			                Trước tiên ta cần chọn dữ liệu cần thiết trong <strong>BẢNG</strong> để tạo nội dung báo cáo.
                        </p>
                    </div>
                    <div class="buttons wow fadeInUp">
                        <a class="btn btn-primary btn-customized scroll-link" href="#section-1" role="button">
                            <i class="fas fa-book-open"></i>Nội dung báo cáo
                        </a>
                        <a class="btn btn-primary btn-customized-2 scroll-link" href="#section-6" role="button">
                            <i class="fas fa-pencil-alt"></i>Tiêu đề báo cáo
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End top content -->

    <!-- Diagrams -->
    <div class="section-1-container section-container" id="section-1">
        <div class="container">
            <div class="row">
                <div class="col section-1 section-description wow fadeIn">
                    <h2>BIỂU ĐỒ</h2>
                    <div class="divider-1 wow fadeInUp"><span></span></div>
                </div>
            </div>
            <div class="tab-content" id="pills-tabContent">
                <div class="tab-pane fade show active" id="relationship-panel" role="tabpanel" aria-labelledby="relationship-panel-tab">
                    <div class="container-draggable card" id="listTable">
                        <canvas id="canvas" height="1" width="1"></canvas>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- End diagrams -->

    <!-- Content -->
    <div class="row mt-5">
        <div class="col-12">
            <form method="get" id="genForm">
                <div class="card">
                    <div class="card-body bg-white">
                        <h2 class="card-title">THIẾT LẬP NỘI DUNG BÁO CÁO</h2>
                        <div class="divider-1 wow fadeInUp"><span></span></div>
                        <p>Nếu điều kiện chuỗi kí tự thì cần định dạng là <strong>N'điều kiện'</strong>.</p>
                        <hr />

                        <!-- Button -->
                        <div class="row">
                            <div class="col col-lg-6">
                                <button type="button" class="btn btn-danger mb-3 mr-3 btn-icon-split" id="genSQL">
                                    <span class="icon text-white-50">
                                        <i class="fas fa-pencil-alt"></i>
                                    </span>
                                    <span class="text">Tạo query</span>
                                </button>

                                <button type="button" class="btn btn-danger mb-3 mr-3 btn-icon-split" id="sumSQL">
                                    <span class="icon text-white-50">
                                        <i class="fas fa-plus"></i>
                                    </span>
                                    <span class="text">Thêm hàm thống kê</span>
                                </button>

                                <button type="button" class="btn btn-danger mb-3 mr-3 btn-icon-split" id="resetAllSQL">
                                    <span class="icon text-white-50">
                                        <i class="fas fa-sync-alt"></i>
                                    </span>
                                    <span class="text">Reset</span>
                                </button>
                            </div>

                            <div class="col col-lg-6">
                                <button type="button" class="btn btn-info mb-3 mr-3 btn-icon-split" id="addOneColumn">
                                    <span class="icon text-white-50">
                                        <i class="fas fa-plus-circle"></i>
                                    </span>
                                    <span class="text">Thêm cột</span>
                                </button>

                                <button type="button" class="btn btn-info mb-3 mr-3 btn-icon-split" id="addOneRow">
                                    <span class="icon text-white-50">
                                        <i class="fas fa-plus-square"></i>
                                    </span>
                                    <span class="text">Thêm hàng</span>
                                </button>

                                <button type="button" class="btn btn-dark mb-3 mr-3 btn-icon-split" id="removeOneColumn">
                                    <span class="icon text-white-50">
                                        <i class="fas fa-minus-circle"></i>
                                    </span>
                                    <span class="text">Xóa cột</span>
                                </button>

                                <button type="button" class="btn btn-dark mb-3 mr-3 btn-icon-split" id="removeOneRow">
                                    <span class="icon text-white-50">
                                        <i class="fas fa-minus-square"></i>
                                    </span>
                                    <span class="text">Xóa hàng</span>
                                </button>
                            </div>
                        </div>
                        <!-- End Button -->

                        <!-- Table -->
                        <table class="table table-bordered table-sm no-border table-responsive" id="dataTable" width="100%" cellspacing="0">
                            <tbody>
                                <% foreach (var col in listCol)
                                    { %>
                                <tr id="tr_<%= col %>" class="<%= col == "Total" ? "d-none" : "" %>">
                                    <td class="no-border" style="width: 100px"><%= col %></td>

                                    <% for (int i = 0; i < 10; i++)
                                        { %>
                                    <td style="width: 150px">
                                        <% if (col == "Table" || col == "Field")
                                            { %>
                                        <select class="form-select form-select-sm gen_<%= col %>" name="gen_<%= col %>">
                                            <option value=""></option>
                                        </select>
                                        <% } %>

                                        <% else if (col == "Show")
                                            { %>
                                        <div class="form-check text-center">
                                            <input class="form-check-input" type="checkbox" value="<%= i %>" name="gen_<%= col %>" />
                                        </div>
                                        <% } %>

                                        <% else if (col == "Sort")
                                            { %>
                                        <select class="form-select form-select-sm gen_<%= col %>" name="gen_<%= col %>">
                                            <option value="">--- Sort ---</option>
                                            <option value="asc">Ascending</option>
                                            <option value="desc">Descending</option>
                                        </select>
                                        <% } %>

                                        <% else if (col == "Total")
                                            { %>
                                        <select class="form-select form-select-sm gen_<%= col %>" name="gen_<%= col %>" disabled>
                                            <option value="group_by">--- Group By ---</option>
                                            <option value="count">Count</option>
                                            <option value="sum">Sum</option>
                                            <option value="min">Min</option>
                                            <option value="max">Max</option>
                                            <option value="avg">Avg</option>
                                            <option value="none">None</option>
                                        </select>
                                        <% } %>


                                        <% else if (col == "Or")
                                            { %>
                                        <input class="form-control form-control-sm gen_<%= col %>" value="" name="gen_<%= col %>" />
                                        <% } %>


                                        <% else
                                            { %>
                                        <input class="form-control form-control-sm gen_<%= col %>" value="" name="gen_<%= col %>" id="gen_<%= col %>" />
                                        <% } %>
                                                </td>
                                    <% } %>
                                </tr>
                                <% } %>

                                <% for (int i = 0; i < 4; i++)
                                    { %>
                                <tr>
                                    <td class="no-border">&nbsp;</td>
                                    <% for (int j = 0; j < 10; j++)
                                        { %>
                                    <td>
                                        <input class="form-control form-control-sm" name="gen_Or" value="" />
                                    </td>
                                    <% } %>
                                </tr>
                                <% } %>
                            </tbody>
                        </table>
                        <!-- End Table -->

                    </div>
                </div>
            </form>
        </div>
    </div>
    <!-- End Content -->


    <!-- Title and query -->
    <div class="section-6-container section-container section-container-image-bg" id="section-6">
        <div class="container">
            <div class="row">
                <div class="col section-6 section-description wow fadeIn">
                    <h2>TẠO BÁO CÁO</h2>
                    <div class="divider-1 wow fadeInUp"><span></span></div>
                    <p>Cần nhập tiêu đề để tạo báo cáo.</p>
                </div>
            </div>
            <div class="section-6-form">
                <form role="form" id="reportForm">
                    <div class="form-group">
                        <label class="sr-only" for="title">Title</label>
                        <input type="text" name="title" placeholder="Title..." class="contact-email form-control" value="">
                    </div>
                    <div class="form-group">
                        <label class="sr-only" for="querySQL">Query SQL</label>
                        <textarea id="querySQL" name="query" rows="7" class="contact-message form-control"></textarea>
                    </div>
                    <button type="submit" class="btn btn-primary btn-customized" id="genReport">
                        <i class="fas fa-paper-plane"></i>
                        <span class="text">In báo cáo</span>
                    </button>
                </form>
            </div>
        </div>
    </div>
    <!-- End title end query -->

</asp:Content>
